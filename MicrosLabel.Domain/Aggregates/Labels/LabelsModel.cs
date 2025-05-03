using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicrosLabel.Domain.Aggregates.Labels
{
    public class LabelsModel : Aggregate
    {

       public LabelsModel(int labelType, string template, string zpl, int dpi, string sql)
        {
            LabelsValidation.ValidateNullStrings(template);
            LabelsValidation.ValidateNullStrings(zpl);
            LabelsValidation.ValidateNullStrings(sql);
            LabelsValidation.SqlQueryValidation(sql);
            LabelsValidation.ZplQueryValidation(zpl, sql);
            LabelsValidation.SqlInyectionOrNullValidation(sql);
            LabelsValidation.LabelTypeValidation(labelType);
            LabelsValidation.DpiValidation(dpi);
;
            LabelType = labelType;
            Template = template;
            Zpl = zpl;
            Dpi = dpi;
            Sql = sql;
        }

        public LabelsModel(string id,int labelType, string template, string zpl, int dpi, string sql)
        {
            LabelsValidation.ValidateNullStrings(template); 
            LabelsValidation.ValidateNullStrings(zpl); 
            LabelsValidation.ValidateNullStrings(sql); 
            LabelsValidation.ValidateNullStrings(id);
            LabelsValidation.SqlQueryValidation(sql);
            LabelsValidation.ZplQueryValidation(zpl, sql);
            LabelsValidation.SqlInyectionOrNullValidation(sql);
            LabelsValidation.LabelTypeValidation(labelType);
            LabelsValidation.DpiValidation(dpi);

            Id = id;
            LabelType = labelType;
            Template = template;
            Zpl = zpl;
            Dpi = dpi;
            Sql = sql;
        }

        public LabelsModel()
        {
           
        }

        public int LabelType { get; set; }

        public string Template { get; set; }

        public string Zpl { get; set; }

        public int Dpi { get; set; }

        public string Sql { get; set; }
    }
}
