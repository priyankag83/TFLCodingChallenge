Feature: Creating and Fetching Users

  Scenario Outline: Validate that the Create API successfully creates a new user record
    Given I set the input data in the expected format using "<name>","<job>"
    When I call the Create User API
    Then The response code should be <expectedReturnCode>
    When I call the Read User API for "new user"
    Then The same record is retrieved using the Read API as per input data
    Examples:
      | name            | job     | expectedReturnCode  | 
      | morpheus        | leader  | 201                 |

  Scenario Outline: Validate the functionality of Read API for fetching User Data returns the correct record for a valid ID
    Given I set the expected data using <id>,"<first_name>","<last_name>","<email>", "<avatar>"
    When I call the Read User API for "<id>"
    Then The response code should be <expectedReturnCode>
    And The same record is retrieved using the Read API
    Examples:
      | id | first_name   | last_name     | email                    | avatar                                                                | expectedReturnCode  | 
      | 2  | Janet        | Weaver        | janet.weaver@reqres.in   | https://s3.amazonaws.com/uifaces/faces/twitter/josephstein/128.jpg    | 200                 |

   Scenario: Validate the functionality of Read Collection API for fetching all Users
    Given The Read User collection API is available
    When I call the Read User collection API
    Then The Read User collection API returns the list of Users

  Scenario: Validate that Put API updates record for the given User
    Given I set the input data in the expected format using "<name>","<job>"
    When I call the Create User API
    And I call the Put User API for Change in values of newly created user id to "<new_name>"
    Then The response code should be <expectedReturnCode>
    When I call the Read User API for "new user"
    Then The same record is retrieved using the Read API as per input data
    Examples:
      | name            | job                      | new_name      | expectedReturnCode  |
      | Priyanka        | leader                   | Priyanka1     | 200                 |

  Scenario: Validate that Read API should not return any record for an invalid ID
    Given The Read API is available
    When I call the Read User API for "23"
    Then The response code should be 404