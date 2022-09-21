using JoberMQ.DataAccess.Abstract.DBTEXT;
using JoberMQ.DataAccess.Repository.DbText.Implementation;
using JoberMQ.Entities.Dbos;

namespace JoberMQ.DataAccess.Implementation.DbText.Default
{
    internal class DfDistributorTextDal : DfDbTextRepository<DistributorDbo>, IDistributorTextDal
    {
        public DfDistributorTextDal(string dbPath, string dbFolderPath, string dbFileName, char dbFileSeparator, char dbArchiveFileSeparator, string dbFileExtension, int maxRowCount) : base(dbPath, dbFolderPath, dbFileName, dbFileSeparator, dbArchiveFileSeparator, dbFileExtension, maxRowCount)
        {
        }
    }
}
