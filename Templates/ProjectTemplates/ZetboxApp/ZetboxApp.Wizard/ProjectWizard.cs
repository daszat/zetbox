// http://code.google.com/p/dot-net-solution-templates/source/browse/trunk/TemplateWizard/Wizard/Wizard.cs

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;
using System.IO;
using EnvDTE80;


namespace ZetboxApp.Wizard
{
    public class ProjectWizard : IWizard
    {
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        public void RunFinished()
        {
            
        }

        public void RunStarted(object automationObject, Dictionary<string, string> replacementsDictionary, WizardRunKind runKind, object[] customParams)
        {
            replacementsDictionary.Add("$safesolutionname$", SolutionWizard.SolutionName);
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }
    }
}
