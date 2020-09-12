# TFLCodingChallenge

## Design decisions
- At the very beginning, I wish to call out that some of the tests are failing, which I believe are failing correctly as the API under test is not behaving properly.
Few instances:
Create Operation: Once Post API is called with a valid payload JSON, we do get 201 along with a response JSON containing id, name, job, createdAt. However when this new user's ID is used in the GET Call for Read operation, the record is not found and the API returns a 404.
Update Operation: Same is the case with Update operation (PUT) as explained in point # 1.

- For validation of 'Read All Users' operation, we have assumed that if the response field "total" is greater than 1 and the "data" field is a collection (type JsonArray), then the returned response contains a list of users.

- Focus has been given to Reusability and modularity which can be seen in code written in a way so that same steps can be used across scenarios.

- Scenarios have been designed in a way so that each of these are independent of each other and can be executed without any dependencies.

- For the purpose of validations, new data is being created before using it for further validation for UPDATE operation. This is being done in order to avoid dependency on existing data.

- Hardcoding of data has been avoided by using Example tables and parameters within the steps.