using MicrosLabel.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MicrosLabel.Domain.Aggregates.Labels
{
    public class LabelsValidation
    {
        //NOT NULL STRINGS
        public static void ValidateNullStrings(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentNullException(Resources.NullT_Z_S);
            }
        }

        //VALIDAR INTS
        public static void DpiValidation(int number)
        {
            if (number <= 0 || number > 3000)
            {
                throw new System.ArgumentException(Resources.OutOfRangeDpi);
            }
        }

        public static void LabelTypeValidation(int number)
        {
            if (number < 0 || number > 4)
            {
                throw new System.ArgumentException(Resources.OutOfRangeLabelType);
            }
        }
        //VALIDACIONES SQL Y ZPL
        public static void SqlInyectionOrNullValidation(string sql)
        {
            string sqlupp = sql.ToUpper();
            {
                if (sqlupp.Contains("DELETE") || sqlupp.Contains("DROP") || sqlupp.Contains("UPDATE")
                || sqlupp.Contains("TRUNCATE") || sqlupp.Contains("DATABASE") || sqlupp.Contains("EXECUTE")
                || sqlupp.Contains("COMMAND"))
                {
                    throw new System.ArgumentException(Resources.SqlInyection);
                }
            }
        }

        public static void SqlQueryValidation(string sql)
        {
            string sqlupp = sql.ToUpper();
            string[] separadas;
            separadas = sqlupp.Split("WHERE");
            if (!sql.Contains("WHERE"))
            {
                throw new ArgumentException(Resources.InvalidSqlConsult);
            }
            if (!separadas[1].Contains("SKU") && separadas[1].Contains("CLIENTCODE"))
            {
                throw new System.ArgumentException(Resources.InvalidSqlConsult);
            }
        }


        public static void ZplQueryValidation(string zpl, string sql)
        {
            bool iguales = true;
            string[] discrimnatorZpl = { "{{", "}}" };
            string[] discrimnatorSql = { ".", ",", " ", " ", " ", "" };
            string[] positionValidation = zpl.ToUpper().Split(discrimnatorZpl, System.StringSplitOptions.RemoveEmptyEntries);
            string[] valores = sql.ToUpper().Split("FROM").First().Split(discrimnatorSql, System.StringSplitOptions.None);
            int con = 0;

            foreach (string word in positionValidation)
            {
                if (con % 2 != 0)
                {
                    iguales = false;
                    foreach (string word2 in valores)
                    {
                        if (word == word2)
                        {
                            iguales = true;
                            break;
                        }
                    }
                    if (!iguales)
                    {
                        throw new System.ArgumentException(Resources.InvalidSqlParameters);
                    }
                }
                con++;
            }
        }
    }
}
