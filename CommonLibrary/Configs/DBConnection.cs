/*******************************************************************************
 *数据库连接
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class DBConnection
    {
        public static bool Encrypt { get; set; }
        public DBConnection(bool encrypt)
        {
            Encrypt = encrypt;
        }
        public static string connectionString
        {
            get
            {
                //web.config 中配置的数据连接名称
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["NFineDbContext"].ConnectionString;
                if (Encrypt == true)
                {
                    return DESEncrypt.Decrypt(connection);
                }
                else
                {
                    return connection;
                }
            }
        }
    }
}
