using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace CassandraTest
{
    class Program
    {
        static void Main(string[] args)
        {

#if false

            var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            var session = cluster.Connect();

            session.Execute("DROP KEYSPACE IF EXISTS simplex");
            session.Execute("CREATE KEYSPACE IF NOT EXISTS simplex WITH replication = { 'class': 'SimpleStrategy', 'replication_factor' : 1};");

            session.ChangeKeyspace("simplex");

            session.Execute(
                    "CREATE TABLE IF NOT EXISTS simplex.songs (" +
                        "id uuid PRIMARY KEY," +
                        "title text," +
                        "album text," +
                        "artist text," +
                        "tags set<text>," +
                        "data blob" +
                        ");");

            session.Execute(
                    "CREATE TABLE IF NOT EXISTS simplex.playlists (" +
                        "id uuid," +
                        "title text," +
                        "album text, " +
                        "artist text," +
                        "song_id uuid," +
                        "PRIMARY KEY (id, title, album, artist)" +
                        ");");

           session.Execute(
                    "INSERT INTO simplex.songs (id, title, album, artist, tags) " +
                    "VALUES (" +
                        "756716f7-2e54-4715-9f00-91dcbea6cf50," +
                        "'La Petite Tonkinoise'," +
                        "'Bye Bye Blackbird'," +
                        "'Joséphine Baker'," +
                        "{'jazz', '2013'})" +
                        ";");

            session.Execute(
                    "INSERT INTO simplex.playlists (id, song_id, title, album, artist) " +
                    "VALUES (" +
                        "2cc9ccb7-6221-4ccb-8387-f22b6a1b354d," +
                        "756716f7-2e54-4715-9f00-91dcbea6cf50," +
                        "'La Petite Tonkinoise'," +
                        "'Bye Bye Blackbird'," +
                        "'Joséphine Baker'" +
                        ");");

            RowSet results = session.Execute("SELECT * FROM simplex.playlists WHERE id = 2cc9ccb7-6221-4ccb-8387-f22b6a1b354d;");

            session.Execute("DROP TABLE IF EXISTS simplex.playlists");
            session.Execute("DROP TABLE IF EXISTS simplex.songs");

            session.DeleteKeyspaceIfExists("simplex");

#else
            
            var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            var session = cluster.Connect();

            session.Execute("DROP KEYSPACE IF EXISTS driver_samples_kp");
            session.Execute("CREATE KEYSPACE IF NOT EXISTS driver_samples_kp WITH replication = { 'class': 'SimpleStrategy', 'replication_factor' : 1};");

//            session.ChangeKeyspace("driver_samples_kp");

            var createCql = @"
                CREATE TABLE driver_samples_kp.temperature_by_day (
                   weatherstation_id text,
                   date text,
                   event_time timestamp,
                   temperature decimal,
                   PRIMARY KEY ((weatherstation_id,date),event_time)
                )";
            session.Execute(createCql);

            var createTopicCql = @"
                CREATE TABLE driver_samples_kp.topics (
                    topic_id uuid PRIMARY KEY,
                    topic_title text,
                    topic_date timestamp
                )";
            session.Execute(createTopicCql);

            var createMessageCql = @"
                CREATE TABLE driver_samples_kp.messages (
                    topic_id uuid,
                    message_date timestamp,
                    message_body text,
                    PRIMARY KEY (topic_id, message_date)
                )";
            session.Execute(createMessageCql);


            session = cluster.Connect("driver_samples_kp");

            {
                //Trying to simulate the insertion of several rows
                //with temperature measures
                for (var i = 0; i < 1; i++)
                {
                    var insertCql = @"
                INSERT INTO temperature_by_day 
                (weatherstation_id, date, event_time, temperature)
                VALUES
                (?, ?, ?, ?)";

                    //Create an insert statement
                    var insertStatement = new SimpleStatement(insertCql);
                    //Bind the parameters to the statement
                    insertStatement.Bind("station1", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now, i / 16M);
                    //You can set other options of the statement execution, for example the consistency level.
                    insertStatement.SetConsistencyLevel(ConsistencyLevel.Quorum);
                    //Execute the insert
                    session.Execute(insertStatement);
                }

                var selectCql = "SELECT * FROM temperature_by_day WHERE weatherstation_id = ? AND date = ?";
                var selectStatement = new SimpleStatement(selectCql);
                selectStatement.Bind("station1", DateTime.Now.ToString("yyyyMMdd"));
                session.Execute(selectStatement);
            }

            // Execute Async ////////////////////////////////////////////////////

            //It is basically the same code as the AddTemperature
            //Except it returns a Task that already started but didn't finished
            //The row would not be in Cassandra yet when this method finishes executing.

            {
                var insertTaskList = new List<Task>();
                for (var i = 0; i < 1000; i++)
                {
                    //It returns a task that is going to be completed when the Cassandra ring acknowledge the insert
                    var insertCql = @"
                INSERT INTO temperature_by_day 
                (weatherstation_id, date, event_time, temperature)
                VALUES
                (?, ?, ?, ?)";

                    //Create an insert statement
                    var insertStatement = new SimpleStatement(insertCql);
                    //Bind the parameters to the statement
                    insertStatement.Bind("station2", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now, i / 16M);
                    //You can set other options of the statement execution, for example the consistency level.
                    insertStatement.SetConsistencyLevel(ConsistencyLevel.Quorum);
                    //Execute the insert
                    insertTaskList.Add(session.ExecuteAsync(insertStatement));
                }

                while (true)
                {
                    if (insertTaskList.Count == 0)
                        break;

                    foreach (var task in insertTaskList.Reverse<Task>())
                    {
                        if (task.IsCompleted)
                            insertTaskList.Remove(task);
                    }

                    System.Threading.Thread.Sleep(3);
                }

                // Task.WaitAny(insertTaskList.ToArray());

                //Now lets retrieve the temperatures for a given date
                var selectCql = "SELECT * FROM temperature_by_day WHERE weatherstation_id = ? AND date = ?";
                //Create a statement
                var selectStatement = new SimpleStatement(selectCql);
                //Add the parameters
                selectStatement.Bind("station2", DateTime.Now.ToString("yyyyMMdd"));
                //Execute the select statement


                var rs = session.Execute(selectStatement);
                
                //lets print a few of them
                Console.WriteLine("Printing the first temperature records (only 20, if available)");
                var counter = 0;
                foreach (var row in rs)
                {
                    Console.Write(row.GetValue<string>("weatherstation_id"));
                    Console.Write("\t");
                    Console.Write(row.GetValue<DateTime>("event_time").ToString("HH:mm:ss.fff"));
                    Console.Write("\t");
                    Console.WriteLine(row.GetValue<decimal>("temperature"));
                    //It is just an example, 20 is enough
                    if (counter++ == 20)
                    {
                        Console.WriteLine();
                        break;
                    }
                }
            }


            // Forum Execution ////////////////
            {
                var _insertTopicStatement = session.Prepare("INSERT INTO topics (topic_id, topic_title, topic_date) VALUES (?, ?, ?)");
                var _insertMessageStatement = session.Prepare("INSERT INTO messages (topic_id, message_date, message_body) VALUES (?, ?, ?)");

                var topicId = Guid.NewGuid();
                var batch = new BatchStatement();

                //bind the parameters on each statement and add them to the batch
                batch.Add(_insertTopicStatement.Bind(topicId, "Sample forum thread", DateTime.Now));
                batch.Add(_insertMessageStatement.Bind(topicId, DateTime.Now, "This is the first message and body of the topic"));

                //You can set other options of the batch execution, for example the consistency level.
                batch.SetConsistencyLevel(ConsistencyLevel.Quorum);
                //Execute the insert of the 2 rows
                session.Execute(batch);

                //Insert some messages
                for (var i = 1; i <250; i++)
                {
                    var boundStatement = _insertMessageStatement.Bind(topicId, DateTime.Now, "Message " + (i + 1));
                    //We can specify execution options for the statement
                    boundStatement.SetConsistencyLevel(ConsistencyLevel.Quorum);
                    //Execute the bound statement
                    session.Execute(boundStatement);
                }

                //Now lets retrieve the messages by topic with a page size of 20.

                //We will add 1 row using a prepared statement.
                var selectCql = "SELECT * FROM messages WHERE topic_id = ?";
                //Prepare the insert message statement and bind the parameters
                var statement = session.Prepare(selectCql).Bind(topicId);

                //We can specify execution options, like page size and consistency
                statement.SetPageSize(100).SetConsistencyLevel(ConsistencyLevel.One);

                //Execute the prepared statement
                var rs = session.Execute(statement);


                //At this point only 100 rows are loaded into the RowSet.
                Console.WriteLine("Printing all the rows paginating with a page size of 100");
                foreach (var row in rs)
                {
                    //While we iterate though the RowSet
                    //We will paginate through all the rows
                    Console.WriteLine(row.GetValue<string>("message_body"));
                }
            
            }
#endif
        }
    }
}
