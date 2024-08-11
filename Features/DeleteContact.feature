Feature: DeleteContact

As a user I want to delete contact in the contact list

@Delete @HappyPath
@DataSource:../Data/ContactDetails.xlsx @DataSet:ValidContacts
Scenario: Delete a valid contact
	Given user is logged in as test30513
	And want to delete existing contact with the following details
	| firstName   | lastName   | dateOfBirth   | email   | phone   | streetAddress1   | streetAddress2   | city   | stateOrProvince   | postalCode   | country   |
	| <FirstName> | <LastName> | <DateOfBirth> | <Email> | <Phone> | <StreetAddress1> | <StreetAddress2> | <City> | <StateOrProvince> | <PostalCode> | <Country> |
	When contact has the following <Email> to be deleted
	Then contact with <Email> is not viewable on the list
	# valid scenario where contact is missing on the list (non existing element)
