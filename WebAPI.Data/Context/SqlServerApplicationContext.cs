using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebAPI.Data.Context
{
    public class SqlServerApplicationContext:DbContext, IApplcationDbContext
    {
       
        public SqlServerApplicationContext(DbContextOptions option)
            : base(option)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ShopG2;Integrated Security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlServerApplicationContext).Assembly);
            modelBuilder.SetCreateOn();
            
            base.OnModelCreating(modelBuilder);
        }

        public override EntityEntry Update([NotNull] object entity)
        {

            return base.Update(entity);
        }
  
        public List<T> RunSp<T>(string StoreName, List<DbParameter> ListParamert) where T : new()
        {
            using var connection = Database.GetDbConnection();
            connection.Open();
            using var cmd = connection.CreateCommand();

          
            cmd.CommandText = StoreName;
            cmd.CommandType = CommandType.StoredProcedure;

            foreach (var item in ListParamert)
            {
                cmd.Parameters.Add(new SqlParameter { ParameterName = item.ParameterName, Value = item.Value });
            }


            List<T> list = new List<T>();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader != null && reader.HasRows)
                {
                    var entity = typeof(T);
                    var propDict = new Dictionary<string, PropertyInfo>();
                    var props = entity.GetProperties
           (BindingFlags.Instance | BindingFlags.Public);
                    propDict = props.ToDictionary(p => p.Name.ToUpper(), p => p);
                    while (reader.Read())
                    {
                        T newobject = new T();

                        for (int index = 0; index < reader.FieldCount; index++)
                        {
                            if (propDict.ContainsKey(reader.GetName(index).ToUpper()))
                            {
                                var info = propDict[reader.GetName(index).ToUpper()];
                                if (info != null && info.CanWrite)
                                {
                                    var val = reader.GetValue(index);
                                    info.SetValue(newobject, val == DBNull.Value ? null : val, null);
                                }
                            }
                        }
                        list.Add(newobject);
                    }

                }
                Database.CloseConnection();
                return list;
            }
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                CleanContext();
                throw ex;
            }
        }

        private void CleanContext()
        {
            if (ChangeTracker.HasChanges())
            {
                var _list = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted).ToList();
                foreach (var item in _list)
                {
                    item.State = EntityState.Unchanged;
                }
            }
        }

       
    }
}
