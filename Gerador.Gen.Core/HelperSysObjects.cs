using Common.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seed.Gen
{
    public class HelperSysObjects : HelperSysObjectsDDD
    {

        public HelperSysObjects(IEnumerable<Context> contexts)
            : base(contexts)
        {

        }
       

        protected override string MakeClassName(TableInfo tableInfo)
        {
            return tableInfo.TableName.Replace("PREFIX_", "");
        }


        #region Config Front

        public override HelperSysObjectsBase DefineFrontTemplateClass(Context config)
        {
            return new HelperSysObjectsAngular20(config, "Templates\\Front", new HelperControlHtmlAngular20Custom());
        }

        public override string TransformFieldString(ConfigExecutetemplate configExecutetemplate, Info info, string propertyName, string textTemplate)
        {
            return base.TransformFieldString(configExecutetemplate, info, propertyName, textTemplate);
        }

        #endregion



    }
}
