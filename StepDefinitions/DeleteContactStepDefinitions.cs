using BoDi;
using DemoSeleniumSpecFlow.DataModel;
using DemoSeleniumSpecFlow.Helper;
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
    [Scope(Feature = "DeleteContact")]
    public class DeleteContactStepDefinitions : BaseStepDefinitions
    {
        public DeleteContactStepDefinitions(IObjectContainer container) : base(container) { }

        [Given(@"want to delete existing contact with the following details")]
        public void GivenWantToDeleteExistingContactWithTheFollowingDetails(Table table)
        {
            ContactDetailsModel contactDetails = table.CreateInstance<ContactDetailsModel>();

            contactListPage.ClickAddContactButton();
            HelperMethod.AddContact(addEditContactPage, contactDetails.FirstName, contactDetails.LastName,
               contactDetails.DateOfBirth, contactDetails.Email, contactDetails.Phone, contactDetails.StreetAddress1,
               contactDetails.StreetAddress2, contactDetails.City, contactDetails.StateOrProvince, contactDetails.PostalCode,
               contactDetails.Country);
        }

        [When(@"contact has the following (.*) to be deleted")]
        public void WhenContactHasTheFollowingToBeDeleted(string email)
        {
            var contactEntry = contactListPage.SearchContactList(email);
            cleanupData.Id = contactListPage.GetContactIdText(contactEntry);
            contactListPage.ClickContactItem(contactEntry);

            contactDetailsPage.ClickDeleteContactButton();
            contactDetailsPage.AcceptAlertDialog();
        }

        [Then(@"contact with (.*) is not viewable on the list")]
        public void ThenContactWithIsNotViewableOnTheList(string email)
        {
            Assert.IsNull(contactListPage.SearchContactList(email));
        }



    }
}
