Feature: Retrieve Member by ID

Scenario: Existing Member
	Given a member exists with IDMember "1e0de03d-8820-43a1-9ada-bba84e8c7fa7"
	When the member is requested by IDMember
	Then the response should contain the member with the same IDMember