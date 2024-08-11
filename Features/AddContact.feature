Feature: AddContact

As a user I want to add contact in the contact list

@Create @Read @HappyPath
@DataSource:../Data/ContactDetails.xlsx @DataSet:ValidContacts
Scenario: Add a valid contact
	Given user is logged in as test0513
	And want to add new contact
	When contact has the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |
	And new contact is saved
	And return to contact list
	Then contact is viewable on the list with the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |

@UnhappyPath
@DataSource:../Data/ContactDetails.xlsx @DataSet:InValidContacts
Scenario: Add an invalid contact
	Given user is logged in as test0513
	And want to add new contact
	When contact has the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |
	And new contact is saved
	Then contact validation failed by <ErrorDetails>

# this ussually is not a valid test as we are not expecting any test to fail
# this test is forcefully failed
@UnhappyPath
Scenario: non existing element force test failure
	Given user is logged in as test0513
	And want to add new contact
	When Enter value to missing element
	Then Test Failure