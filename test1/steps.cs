using System;
using TechTalk.SpecFlow;
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace test1
{
    [Binding]
    public class steps
    {
        dynamic jobject;
        dynamic jobject_new;
        dynamic expData;
        dynamic inputData;
        int responseStatusCode;
        int totalUserRecords;
        RestRequest restRequest;
        IRestResponse restResponse;
        string jobject_id;
        RestClient restClient = new RestClient("https://reqres.in/api/users/");

        [Given(@"I set the input data in the expected format using ""(.*)"",""(.*)""")]
        public void GivenISetTheInputDataInTheExpectedFormatUsing(string name, string job)
        {
            inputData = new JObject();
            inputData.name = name;
            inputData.job = job;
            Console.WriteLine(inputData);
            Console.WriteLine(inputData.GetType());
            Console.WriteLine(inputData.ToString());
        }

        [When(@"I call the Create User API")]
        public void WhenICallTheCreateUserAPI()
        {
            Console.WriteLine("Calling Create Request Method");
            string response = null;
          
                restRequest = new RestRequest(Method.POST);
                restRequest.AddParameter("application/json", inputData, ParameterType.RequestBody);
                restResponse = restClient.Execute(restRequest);
                response = restResponse.Content;
                responseStatusCode = (int)restResponse.StatusCode;

                Console.WriteLine(response);
                Console.WriteLine(response.GetType());

                Console.WriteLine(responseStatusCode);
                Console.WriteLine(responseStatusCode.GetType());

                if (response != "{}")
                {
                    Console.WriteLine("Non-Empty Response is returned");

                    jobject = JsonConvert.DeserializeObject<JObject>(response);
                    jobject_id = jobject["id"];
                    Console.WriteLine(jobject);
                    Console.WriteLine(jobject.GetType());
                }
        }

        [When(@"I call the Put User API for Change in values of newly created user id to ""(.*)""")]
        public void WhenICallThePutUserAPI(string new_name)
        {
            Console.WriteLine("Calling Put Request Method with name change to: " + new_name);
            string uid = jobject_id;
            Console.WriteLine(uid);

            string response = null;

            inputData.name = new_name;
            Console.WriteLine(inputData);

            //uid of newly created User has been passed, for name change in it.
            restRequest = new RestRequest(uid, Method.PUT);
            restRequest.AddParameter("application/json", inputData, ParameterType.RequestBody);
            restResponse = restClient.Execute(restRequest);
            response = restResponse.Content;
            responseStatusCode = (int) restResponse.StatusCode;

            Console.WriteLine(response);
            Console.WriteLine(response.GetType());

            Console.WriteLine(responseStatusCode);
            Console.WriteLine(responseStatusCode.GetType());

            if (response != "{}")
            {
                Console.WriteLine("Non-Empty Response is returned");
                jobject = JsonConvert.DeserializeObject<JObject>(response);
                Console.WriteLine(jobject);
                Console.WriteLine(jobject.GetType());
            }
        }


        [Then(@"The response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedReturnCode)
        {
            Console.WriteLine(responseStatusCode);
            Console.WriteLine(expectedReturnCode);
            Assert.AreEqual(responseStatusCode, expectedReturnCode);
        }

        [When(@"I call the Read User API for ""(.*)""")]
        public void WhenICallTheReadUserAPIFor(string uid)
        {
            Console.WriteLine("Calling Get with id:" + uid);
            if (uid == "new user")
            {
                uid = jobject_id; //this is newly created id          
            }
            Console.WriteLine("Calling Get with id:" + uid);
            restRequest = new RestRequest(uid, Method.GET);
            // Executing request to server and checking server response to the it
            restResponse = restClient.Execute(restRequest);

            // Extracting output data from received response
            string response = restResponse.Content;
            responseStatusCode = (int)restResponse.StatusCode;

            Console.WriteLine(response);
            Console.WriteLine(response.GetType());

            Console.WriteLine(responseStatusCode);
            Console.WriteLine(responseStatusCode.GetType());

            if (response != "{}")
            {
                Console.WriteLine("Non-Empty Response is returned");
                jobject = JsonConvert.DeserializeObject<JObject>(response);
                jobject_id = jobject["data"]["id"];
                Console.WriteLine(jobject.GetType());
                Console.WriteLine(jobject["data"]);
                Console.WriteLine(jobject["data"]["id"]);
            }
            else
            {
                Console.WriteLine("No Such Data Exist");
            }

        }

        [Then(@"The same record is retrieved using the Read API as per input data")]
        public void ThenTheSameRecordIsRetrievedUsingTheReadAPIAsPerInputData()
        {
            Console.WriteLine(inputData.GetType());
            Console.WriteLine(inputData);

            Console.WriteLine(jobject.GetType());
            Console.WriteLine(jobject["data"]);

            Assert.AreEqual(jobject["data"], inputData);
        }

        [Then(@"The same record is retrieved using the Read API")]
        public void ThenTheSameRecordIsRetrievedUsingTheReadAPI()
        {
            Console.WriteLine(expData.GetType());
            Console.WriteLine(expData);

            Console.WriteLine(jobject.GetType());
            Console.WriteLine(jobject["data"]);

            Assert.AreEqual(jobject["data"], expData);
        }

        [Given(@"The Read API is available")]
        public void GivenTheReadAPIIsAvailable()
        {
            Console.WriteLine("Calling Read API...");
        }

        [Given(@"I set the expected data using (.*),""(.*)"",""(.*)"",""(.*)"", ""(.*)""")]
        public void GivenISetTheExpectedDataInTheExpectedFormatUsing(int id, string first_name, string last_name, string email, string avatar)
        {
            expData = new JObject();
            expData.id = id;
            expData.email = email;
            expData.first_name = first_name;
            expData.last_name = last_name;
            expData.avatar = avatar;
            Console.WriteLine(expData);
            Console.WriteLine(expData.GetType());
            Console.WriteLine(expData.ToString());
        }

        [Given(@"The Read User collection API is available")]
        public void GivenTheReadCollectionAPIIsAvailable()
        {
            Console.WriteLine("Calling Read Collection API...");
        }

        [When(@"I call the Read User collection API")]
        public void WhenICallTheReadCollectionAPI()
        {
            RestRequest restRequest = new RestRequest(Method.GET);

            // Executing request to server and checking server response to the it
            IRestResponse restResponse = restClient.Execute(restRequest);

            // Extracting output data from received response
            string response = restResponse.Content;
            responseStatusCode = (int)restResponse.StatusCode;

            Console.WriteLine(response);
            Console.WriteLine(response.GetType());

            Console.WriteLine(responseStatusCode);
            Console.WriteLine(responseStatusCode.GetType());

            if (response != "{}")
            {
                Console.WriteLine("Non-Empty Response is returned");
                jobject = JsonConvert.DeserializeObject<JObject>(response);
                Console.WriteLine(jobject.GetType());
                Console.WriteLine(jobject["data"].GetType());
            }
        }

        [Then(@"The Read User collection API returns the list of Users")]
        public void ThenTheReadCollectionAPIReturnsTheListOfUsers()
        {
            Console.WriteLine(jobject["total"]);
            Console.WriteLine(jobject["data"].GetType());
            //Checking if Collection is returned with Total User Count > 1
            Assert.Greater(jobject["total"], 1);
            //Also Checking if Data returned is a JSON Array as its a collection
            Assert.AreEqual(jobject["data"].GetType().ToString(), "Newtonsoft.Json.Linq.JArray");
        }
    }
}


