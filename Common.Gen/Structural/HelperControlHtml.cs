using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen.Structural
{
    public abstract class HelperControlHtml
    {
        public abstract string MakeInputFilterHtml(string attr = "");
        public abstract string MakeInputHtml(Context configContext, TableInfo tableInfo, Info info, bool isStringLengthBig, string propertyName);

        public abstract string MakeDateTimePiker(bool v, string attrFieldConfig);

        public abstract string MakeDatePiker(bool v, string attrFieldConfig);
        public abstract string MakeRadioFilter();
        public abstract string MakeRadio(bool v, string attrFieldConfig);
        public abstract string MakeCheckbox(bool v, string attrFieldConfig);
        public abstract string MakeDropDownFilter(string attrFieldConfig);
        public abstract string MakeMultiSelectFilter();
        public abstract string MakeDropDownSeachFilter();
        public abstract string MakeDropDown(bool v, string attrFieldConfig);
        public abstract string MakeDropDownSeach(bool v, string attrFieldConfig);
        public abstract string MakeCtrl(string htmlField);
        public abstract string MakeUpload(Info info);
        public abstract string MakeTextStyle(bool v);
        public abstract string MakeTextEditor(bool v);
        public abstract string MakeTextTag(bool v);
        public abstract string MakeDatePikerFilter(string propertyName = "<#propertyName#>");
    }
}
