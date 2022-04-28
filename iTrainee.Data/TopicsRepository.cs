using iTrainee.Data.DataManager;
using iTrainee.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace iTrainee.Data
{
    public class TopicsRepository : ITopicsRepository
    {
        IDataManager _dataManager = null;

        public TopicsRepository(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public List<Topics> GetAllTopics()
        {
            List<Topics> topicsList = new List<Topics>();
            try
            {
                List<SqlParameter> parameters = null;
                DataSet result = _dataManager.ExecuteStoredProcedure("spInsertStream", parameters);
                if (result.Tables.Count != 0)
                {
                    DataTable dataTable = result.Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            Topics topic = new Topics();
                            topic.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                            topic.StreamId = Convert.ToInt32(dataTable.Rows[i]["StreamId"]);
                            topic.Name = dataTable.Rows[i]["Name"].ToString();
                            topic.ReferenceURL = dataTable.Rows[i]["ReferenceURL"].ToString();
                            topic.InsertedBy = dataTable.Rows[i]["InsertedBy"].ToString();
                            topic.UpdatedBy = dataTable.Rows[i]["UpdatedBy"].ToString();
                            DateTime insertedDate = DateTime.Parse(dataTable.Rows[i]["InsertedOn"].ToString());
                            DateTime updatedDate = DateTime.Parse(dataTable.Rows[i]["UpdatedOn"].ToString());
                            topic.InsertedOn = insertedDate.Date;
                            topic.UpdatedOn = updatedDate.Date;


                            topicsList.Add(topic);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return topicsList;
        }
    }
}
