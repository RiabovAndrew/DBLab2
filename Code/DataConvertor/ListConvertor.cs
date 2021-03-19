using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfBDLab2.DataConvertor {
    class ListConvertor {
        public static string ConvertToString(IEnumerable<string> list) { return ConvertToString(list, "\n"); }

        public static string ConvertToString(IEnumerable<string> list, string separator) {
            return list.Aggregate("", (current, VARIABLE) => current + (VARIABLE + separator));
        }
    }
}