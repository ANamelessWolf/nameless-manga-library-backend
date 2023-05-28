using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.MangaBI.Models
{
    public class RequestStoredProcedure
    {
        [Required]
        public string SpName { get; set; }
        public SqlParameter[] Parameters { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
