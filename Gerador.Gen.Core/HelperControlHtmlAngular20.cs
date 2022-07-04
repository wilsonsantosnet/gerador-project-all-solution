using Common.Gen;
using Common.Gen.Structural;
using System;

namespace Seed.Gen
{
    public class HelperControlHtmlAngular20Custom : HelperControlHtmlAngular20
    {


        public override string MakeDatePiker(bool isRequired, string attr)
        {
            var required = isRequired ? "required" : string.Empty;
            var attrCompose = $"{attr}";
            return base.MakeInputType(required, "text", attrCompose);
        }

        public override string MakeDateTimePiker(bool isRequired, string attr)
        {
            var required = isRequired ? "required" : string.Empty;
            var attrCompose = $"{attr}";
            return base.MakeInputType(required, "datetime-local", attrCompose);
        }


        public override string MakeDropDown(bool isRequired, string attr)
        {
            var attrCustom = $"[enabledSelect2]=false {attr}";
            return base.MakeDropDown(isRequired, attrCustom);
        }

        public override string MakeTextEditor(bool isRequired)
        {
            return base.MakeTextEditor(isRequired);
        }



    }
}
