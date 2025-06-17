Feature: Retrieve Member by ID

Scenario: Existing Member
	Given a member exists with IDMember "6A6969B6-C665-452E-AE7B-8E33FFA0B651"
	When the member is requested by IDMember
	Then the response should contain the member with the same IDMember