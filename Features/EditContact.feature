Feature: EditContact

As a user I want to edit contact in the contact list

@Update @HappyPath
@DataSource:../Data/ContactDetails.xlsx @DataSet:ValidContacts
Scenario: Edit a valid contact
	Given user is logged in as test20513
	And want to edit existing contact
	When contact has the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |
	And edited contact is saved
	And return to contact list
	Then contact is viewable on the list with the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |

@UnhappyPath
@DataSource:../Data/ContactDetails.xlsx @DataSet:InValidContacts
Scenario: Edit an invalid contact
	Given user is logged in as test20513
	And want to edit existing contact
	When contact has the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |
	And edited contact is saved
	Then contact validation failed by <ErrorDetails>