using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class CeresPayload
    {
        [FunctionName("CeresPayload")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<CeresPayloadJson>(requestBody);

            var physicalLocation = data.PhysicalLocation;
            var primaryEngineer = data.PrimaryEngineerOwner;

            var outPut = new OutVars();
            outPut.EngineerDetails = primaryEngineer;
            outPut.PhysicalLocation = physicalLocation;

            return outPut != null
                ? (ActionResult)new OkObjectResult(outPut)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");        }
    }
    public class CeresPayloadJson
    {
        public Guid Id { get; set; }
        public string Rid { get; set; }
        public string Self { get; set; }
        public long Ts { get; set; }
        public string Etag { get; set; }
        public Guid NominationId { get; set; }
        public string ProgramName { get; set; }
        public object Name { get; set; }
        public Customer Customer { get; set; }
        public object NominationOwner { get; set; }
        public string[] ProjectCloudTypes { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset ExpectedCompletionDate { get; set; }
        public object Category { get; set; }
        public string[] Categories { get; set; }
        public string ConditionsOfSuccess { get; set; }
        public object CustomerMilestone { get; set; }
        public string Title { get; set; }
        public object TShirtSize { get; set; }
        public string Description { get; set; }
        public bool IsPartnerDriven { get; set; }
        public bool IsProjectAtRisk { get; set; }
        public bool IsConditionOfSuccessMet { get; set; }
        public bool CustomerStoryOpportunityIndicator { get; set; }
        public object IsCustomerInactive { get; set; }
        public string MiningPotential { get; set; }
        public string CustomerStoryType { get; set; }
        public object[] AzureSubscriptions { get; set; }
        public object[] Tags { get; set; }
        public PrimaryEngineerOwner PrimaryEngineerOwner { get; set; }
        public PrimaryEngineerOwner PrimaryPmOwner { get; set; }
        public object[] SecondaryOwners { get; set; }
        public object[] TechFlags { get; set; }
        public object[] EngineerCharacteristics { get; set; }
        public string State { get; set; }
        public string ExpectedEndState { get; set; }
        public string Status { get; set; }
        public string[] StatusReasonCodes { get; set; }
        public string StatusReasonDescription { get; set; }
        public object ProjectRiskDescription { get; set; }
        public object FutureOpportunities { get; set; }
        public object OpportunityDetails { get; set; }
        public Uri DocumentFolderLink { get; set; }
        public string PhysicalLocation { get; set; }
        public object QualifiedSubscriptionId { get; set; }
        public object QualifiedEnrollmentId { get; set; }
        public object QualifiedSalesLocation { get; set; }
        public object QualifiedSalesUnit { get; set; }
        public object QualifiedSegment { get; set; }
        public object QualifiedIndustry { get; set; }
        public object QualifiedVertical { get; set; }
        public object[] DomainTags { get; set; }
        public object QualifiedIsHiPoAccount { get; set; }
        public object QualifiedIsS400Account { get; set; }
        public object NominatedIsS400Account { get; set; }
        public Guid ProjectId { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public long Version { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string InternalModifiedBy { get; set; }
        public Guid PartitionKey { get; set; }
        public string EventType { get; set; }
        public bool IsEntityDeleted { get; set; }
        public string FriendlyName { get; set; }
        public Contributor[] Contributors { get; set; }
        public Guid Cid { get; set; }
        public object[] ProjectContacts { get; set; }
        public object[] ProjectPartners { get; set; }
        public Activity[] Activities { get; set; }
        public Metadata Metadata { get; set; }
        public long Lsn { get; set; }
    }

    public class OutVars
    {
        public string PhysicalLocation { get; set; }
        public PrimaryEngineerOwner EngineerDetails { get; set; }
    }

    public class Activity
    {
        public string Name { get; set; }
        public PrimaryEngineerOwner AssignedTo { get; set; }
        public string ParentType { get; set; }
        public string ActivityState { get; set; }
        public string Status { get; set; }
        public string ActivityType { get; set; }
        public double TimeSpentInHours { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset ScheduledDate { get; set; }
        public DateTimeOffset CompletedDate { get; set; }
        public bool IsInPersonDelivery { get; set; }
        public object[] DomainTags { get; set; }
        public Guid ActivityId { get; set; }
        public bool IsEntityDeleted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public long Version { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class PrimaryEngineerOwner
    {
        public string EmailAddress { get; set; }
        public object[] BusinessPhones { get; set; }
        public object[] MobilePhones { get; set; }
        public object[] Faxes { get; set; }
        public ProgramIdentifier ProgramIdentifier { get; set; }
        public ProgramIdentifier[] ProgramIdentifiers { get; set; }
        public AccountIdentifier AccountIdentifier { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string ProgramRole { get; set; }
        public string Department { get; set; }
        public bool KeyContact { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string EventType { get; set; }
        public Guid UserId { get; set; }
        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public object ContactPreference { get; set; }
        public object Role { get; set; }
        public object CustomerIdentifier { get; set; }
        public object PartnerIdentifier { get; set; }
        public object Version { get; set; }
        public object XCorrelationId { get; set; }
    }

    public class AccountIdentifier
    {
        public string Type { get; set; }
        public string Name { get; set; }
    }

    public class ProgramIdentifier
    {
        public string Name { get; set; }
        public object Id { get; set; }
        public object Type { get; set; }
    }

    public class Contributor
    {
        public string EmailAddress { get; set; }
        public object[] BusinessPhones { get; set; }
        public object[] MobilePhones { get; set; }
        public object[] Faxes { get; set; }
        public object ContactPreference { get; set; }
        public object Role { get; set; }
        public object CustomerIdentifier { get; set; }
        public ProgramIdentifier ProgramIdentifier { get; set; }
        public ProgramIdentifier[] ProgramIdentifiers { get; set; }
        public object PartnerIdentifier { get; set; }
        public object AccountIdentifier { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public object ProgramRole { get; set; }
        public string Department { get; set; }
        public bool KeyContact { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public object Version { get; set; }
        public string EventType { get; set; }
        public object XCorrelationId { get; set; }
        public Guid UserId { get; set; }
        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Customer
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public object[] SubscriptionIds { get; set; }
        public string Name { get; set; }
        public object IsDeleted { get; set; }
    }

    public class Metadata
    {
    }
}
