using Common.Gen;
using Common.Gen.Helpers;
using Common.Gen.Utils;
using System.Collections.Generic;

namespace Seed.Gen
{
    public class ConfigExternalResources
    {
        private readonly string _basicPathProject;

        public ConfigExternalResources()
        {
            this._basicPathProject = ConfigurationManager.AppSettings[string.Format("PathProject")];
        }


        private ExternalResource GeradorTemplateBack(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "gerador-template-back",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-template-back.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\Gerador.Gen.Core\Templates\Back"),
            };
        }


        private ExternalResource GeradorTemplateFront(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "gerador-template-front",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-template-front.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\Gerador.Gen.Core\Templates\Front"),
            };
        }


        private ExternalResource GeradorFrameworkBack(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                OnlyFoldersContainsThisName = "Common",
                ResouceRepositoryName = "gerador-framework-back",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-framework-back.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\"),
            };

        }


        private ExternalResource GeradorFrameworkFront(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                ResouceRepositoryName = "gerador-framework-front",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-framework-front.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios\"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\Seed.Spa.Ui\src\app\common"),
            };
        }

        
        private ExternalResource GeradorProjectAdminFront(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "gerador-project-admin-front",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-project-admin-front.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\Seed.Spa.Ui\"),
            };
        }
      

        private ExternalResource GeradorProjectSiteFront(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = replaceLocalFilesApplication,
                DownloadOneTime = true,
                DownloadOneTimeFileVerify = "package.json",
                ResouceRepositoryName = "gerador-project-site-front",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-project-site-front.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios\"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\Seed.Spa.Ui.Custom"),
            };
        }



        private ExternalResource GeradorProjectAdminFrontThisFiles(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "gerador-project-admin-front",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-project-admin-front.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution\Seed.Spa.Ui\"),
                OnlyThisFiles = new List<string> {
                    HelperUri.CombineRelativeUri(@"package.json"),
                    HelperUri.CombineRelativeUri(@"web.config"),
                    HelperUri.CombineRelativeUri(@"angular.json"),
                    HelperUri.CombineRelativeUri(@"src\app\app.component.css"),
                    HelperUri.CombineRelativeUri(@"src\app\app.component.html"),
                    HelperUri.CombineRelativeUri(@"src\app\app.component.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\app.module.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\global.service.culture.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\global.service.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\startup.service.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\util\util-shared.module.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\main\main.component.css"),
                    HelperUri.CombineRelativeUri(@"src\app\main\main.component.html"),
                    HelperUri.CombineRelativeUri(@"src\app\main\main.component.ts"),
                    HelperUri.CombineRelativeUri(@"src\app\main\main.service.ts"),
                    HelperUri.CombineRelativeUri(@"src\assets\jquery.nestable.js"),
                    HelperUri.CombineRelativeUri(@"src\app\util\enum\enum.service.ts"),
                }
            };
        }

        private ExternalResource GeradorProjectAllSolutionThisFiles(bool replaceLocalFilesApplication)
        {
            return new ExternalResource
            {
                ReplaceLocalFilesApplication = true,
                ResouceRepositoryName = "gerador-project-all-solution",
                ResourceUrlRepository = "https://github.com/wilsonsantosnet/gerador-project-all-solution.git",
                ResourceLocalPathFolderExecuteCloning = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"Outros\Repositorios"),
                ResourceLocalPathDestinationFolrderApplication = HelperUri.CombineAbsoluteUri(this._basicPathProject, @"gerador-project-all-solution"),
                OnlyThisFiles = new List<string> {
                    HelperUri.CombineRelativeUri(@"Itens Solutions\Common.Domain.dll"),
                    HelperUri.CombineRelativeUri(@"Itens Solutions\Common.Gen.dll"),
                    HelperUri.CombineRelativeUri(@"Itens Solutions\Common.dll"),
                }
            };

        }


        public IEnumerable<ExternalResource> GetConfigExternarReources()
        {
            var replaceLocalFilesApplication = true;
            return StackV30(replaceLocalFilesApplication);
        }

        private IEnumerable<ExternalResource> StackV30(bool replaceLocalFilesApplication)
        {
            return new List<ExternalResource>
            {
               GeradorTemplateBack(replaceLocalFilesApplication),
               GeradorTemplateFront(replaceLocalFilesApplication),
               GeradorFrameworkBack(replaceLocalFilesApplication),
               GeradorFrameworkFront(replaceLocalFilesApplication),
               GeradorProjectAdminFront(replaceLocalFilesApplication),
               GeradorProjectSiteFront(replaceLocalFilesApplication),

               GeradorProjectAdminFrontThisFiles(replaceLocalFilesApplication),
               GeradorProjectAllSolutionThisFiles(replaceLocalFilesApplication)
            };
        }
    }
}
