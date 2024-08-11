using BoDi;
using DemoSeleniumSpecFlow.DataModel;
using DemoSeleniumSpecFlow.Helper;
using DemoSeleniumSpecFlow.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace DemoSeleniumSpecFlow.StepDefinitions
{
    [Binding]
    [Scope(Feature = "EditContact")]
    public class EditContactStepDefinitions : BaseStepDefinitions
    {
        public EditContactStepDefinitions(IObjectContainer container) : base(container) { }

        [Given(@"want to edit existing contact")]
        public void GivenWantToEditExistingContact()
        {
            contactListPage.ClickAddContactButton();
            var id = DateTimeOffset.Now.ToString("ddHHmmss");
            HelperMethod.AddDefaultContact(addEditContactPage, id);
            var contactEntry = contactListPage.SearchContactList($"defaultemail{id}@gmail.com");

            // Save Email for cleanup later
            cleanupData.Id = contactListPage.GetContactIdText(contactEntry);

            contactListPage.ClickContactItem(contactEntry);
            contactDetailsPage.ClickEditContactButton();
        }

        [When(@"edited contact is saved")]
        public void WhenEditedContactIsSaved()
        {
            addEditContactPage.ClickSubmitButton();

        }

        [When(@"return to contact list")]
        public void WhenReturnToContactList()
        {
            contactDetailsPage.ClickReturnButton();
            contactListPage.WaitTableUpdate(cleanupData.Email);
        }
    }
}
