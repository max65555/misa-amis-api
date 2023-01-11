using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common
{
    public static class StoreProcedureName
    {
        /// <summary>
        /// insert base store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// update base store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// update base store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_DELETE = "Proc_{0}_Delete";

        /// <summary>
        /// update base store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_READ = "Proc_{0}_Read";

        /// <summary>
        /// newest Code store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_READ_BY_ID = "Proc_{0}_ReadByID";

        /// <summary>
        /// update base store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_FILTER = "Proc_{0}_FILTER";

        /// <summary>
        /// newest Code store procedure name
        /// </summary>
        public static String PROCEDURE_NAME_NEWEST_CODE = "Proc_{0}_newestCode";

        

    }
}
