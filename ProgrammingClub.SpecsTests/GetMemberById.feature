Feature: Retrieve Member by ID


Scenario: Existing Member
	Given a member exists with IDMember "5270dda8-5041-4002-af0f-e1c3488e868e"
	When the member is requested by IDMember
	Then the response should contain the member with the same IDMember