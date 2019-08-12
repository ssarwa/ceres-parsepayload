using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            var outPut = new OutVars();
            try
            {
                var data = JsonConvert.DeserializeObject<CeresPayloadJson>(requestBody);
                if (data == null)
                {
                    log.LogError("Failed to deserialize input" + requestBody);
                    return new BadRequestObjectResult("Failed to deserialize input");
                }

                var engagementDescription = data.Engagements[0]?.Description;
                var engagementTitle = data.Engagements[0]?.Title;
                var activityName = data.Engagements[0]?.Activities[0]?.Title;

                outPut.CustomerName = data.Customer?.Name;
                outPut.EngineerAlias = data.PrimaryEngineerOwner?.EmailAddress;
                outPut.PhysicalLocation = data.PhysicalLocation ?? "";
                outPut.ActivityName = activityName ?? "";
                outPut.Description = data.Description ?? "";
                outPut.EngagementDescription = engagementDescription;
                outPut.EngagementTitle = engagementTitle;
                outPut.Title = data.Title ?? "";

                if (String.IsNullOrEmpty(activityName.ToString()) && String.IsNullOrEmpty(engagementTitle.ToString()))
                {
                    outPut.IsNewProject = true;
                }
                else if (String.IsNullOrEmpty(activityName.ToString()) && !String.IsNullOrEmpty(engagementTitle.ToString()))
                {
                    outPut.IsNewEngagement = true;
                }
                else
                {
                    outPut.IsNewEngagement = false;
                    outPut.IsNewProject = false;
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.InnerException.ToString());
            }

            return outPut != null
                ? (ActionResult)new OkObjectResult(outPut)
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
    public class OutVars
    {
        public string CustomerName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhysicalLocation { get; set; }
        public string EngineerAlias { get; set; }
        public string EngagementDescription { get; set; }
        public string EngagementTitle { get; set; }
        public string ActivityName { get; set; }
        public bool IsNewProject { get; set; }
        public bool IsNewEngagement { get; set; }
    }

    public class Customer
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public object Value { get; set; }
        public List<object> SubscriptionIds { get; set; }
        public string Name { get; set; }
        public object IsDeleted { get; set; }
    }

    public class PrimaryPMOwner
    {
        public string EmailAddress { get; set; }
        public List<object> BusinessPhones { get; set; }
        public List<object> MobilePhones { get; set; }
        public List<object> Faxes { get; set; }
        public object ContactPreference { get; set; }
        public object Role { get; set; }
        public object CustomerIdentifier { get; set; }
        public object PartnerIdentifier { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public object ProgramRole { get; set; }
        public string Department { get; set; }
        public bool KeyContact { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public object Version { get; set; }
        public string EventType { get; set; }
        public object XCorrelationId { get; set; }
        public string UserId { get; set; }
        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Contributor
    {
        public string EmailAddress { get; set; }
        public List<object> BusinessPhones { get; set; }
        public List<object> MobilePhones { get; set; }
        public List<object> Faxes { get; set; }
        public object ContactPreference { get; set; }
        public object Role { get; set; }
        public object CustomerIdentifier { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public object ProgramRole { get; set; }
        public string Department { get; set; }
        public bool KeyContact { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public object Version { get; set; }
        public string EventType { get; set; }
        public object XCorrelationId { get; set; }
        public string UserId { get; set; }
        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AssignedTo
    {
        public string EmailAddress { get; set; }
        public List<object> BusinessPhones { get; set; }
        public List<object> MobilePhones { get; set; }
        public List<object> Faxes { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public bool KeyContact { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string EventType { get; set; }
        public string UserId { get; set; }
        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Activity
    {
        public string Name { get; set; }
        public AssignedTo AssignedTo { get; set; }
        public string ParentType { get; set; }
        public string ActivityState { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ActivityType { get; set; }
        public double TimeSpentInHours { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public bool IsInPersonDelivery { get; set; }
        public List<object> DomainTags { get; set; }
        public string ActivityId { get; set; }
        public bool IsEntityDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Version { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class PrimaryEngineerOwner
    {
        public string EmailAddress { get; set; }
        public List<object> BusinessPhones { get; set; }
        public List<object> MobilePhones { get; set; }
        public List<object> Faxes { get; set; }
        public object ContactPreference { get; set; }
        public object Role { get; set; }
        public object CustomerIdentifier { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public object ProgramRole { get; set; }
        public string Department { get; set; }
        public bool KeyContact { get; set; }
        public DateTime CreatedOn { get; set; }
        public object CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public object ModifiedBy { get; set; }
        public object Version { get; set; }
        public object EventType { get; set; }
        public object XCorrelationId { get; set; }
        public string UserId { get; set; }
        public string Upn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Engagement
    {
        public string ProjectName { get; set; }
        public List<object> SecondaryOwners { get; set; }
        public string ConditionsOfSuccess { get; set; }
        public string Status { get; set; }
        public bool IsConditionOfSuccessMet { get; set; }
        public string StatusReason { get; set; }
        public List<object> StatusReasons { get; set; }
        public string Priority { get; set; }
        public List<object> SubscriptionIds { get; set; }
        public bool IsRiskAndRecommendationsProvided { get; set; }
        public string RiskDetails { get; set; }
        public string RecommendationDetails { get; set; }
        public bool IsIPUsed { get; set; }
        public bool IsIPModified { get; set; }
        public string Scenario { get; set; }
        public List<object> Services { get; set; }
        public List<object> DCRegions { get; set; }
        public List<object> TechFlags { get; set; }
        public List<object> EngineerCharacteristics { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<object> RiskOrRecommendationType { get; set; }
        public bool RiskMitigation { get; set; }
        public string RiskMitigationDescription { get; set; }
        public string ObservationDescription { get; set; }
        public List<object> DomainTags { get; set; }
        public string Title { get; set; }
        public string EngagementId { get; set; }
        public bool IsEntityDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Version { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public List<Activity> Activities { get; set; }
        public List<object> EngagementContacts { get; set; }
        public PrimaryEngineerOwner PrimaryEngineerOwner { get; set; }
        public string StatusReasonDescription { get; set; }
        public object IPName { get; set; }
        public object AdditionalTopicsCovered { get; set; }
        public object ServiceGroup { get; set; }
        public object ARMTemplateId { get; set; }
        public object RiskSeverity { get; set; }
    }

    public class CeresPayloadJson
    {
        public string id { get; set; }
        public string _rid { get; set; }
        public string _self { get; set; }
        public int _ts { get; set; }
        public string _etag { get; set; }
        public string NominationId { get; set; }
        public string ProgramName { get; set; }
        public object Name { get; set; }
        public Customer Customer { get; set; }
        public object NominationOwner { get; set; }
        public List<string> ProjectCloudTypes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
        public object Category { get; set; }
        public List<object> Categories { get; set; }
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
        public List<object> AzureSubscriptions { get; set; }
        public List<object> Tags { get; set; }
        public PrimaryEngineerOwner PrimaryEngineerOwner { get; set; }
        public PrimaryPMOwner PrimaryPMOwner { get; set; }
        public List<object> SecondaryOwners { get; set; }
        public List<object> TechFlags { get; set; }
        public List<object> EngineerCharacteristics { get; set; }
        public string State { get; set; }
        public string ExpectedEndState { get; set; }
        public string Status { get; set; }
        public List<object> StatusReasonCodes { get; set; }
        public object StatusReasonDescription { get; set; }
        public object ProjectRiskDescription { get; set; }
        public object FutureOpportunities { get; set; }
        public object OpportunityDetails { get; set; }
        public string DocumentFolderLink { get; set; }
        public string PhysicalLocation { get; set; }
        public object QualifiedSubscriptionId { get; set; }
        public object QualifiedEnrollmentId { get; set; }
        public string QualifiedSalesLocation { get; set; }
        public string QualifiedSalesUnit { get; set; }
        public string QualifiedSegment { get; set; }
        public string QualifiedIndustry { get; set; }
        public string QualifiedVertical { get; set; }
        public List<object> DomainTags { get; set; }
        public string QualifiedIsHiPoAccount { get; set; }
        public string QualifiedIsS400Account { get; set; }
        public object NominatedIsS400Account { get; set; }
        public string ProjectId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string Version { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string InternalModifiedBy { get; set; }
        public string PartitionKey { get; set; }
        public string eventType { get; set; }
        public bool IsEntityDeleted { get; set; }
        public string FriendlyName { get; set; }
        public List<Contributor> Contributors { get; set; }
        public string _cid { get; set; }
        public List<object> ProjectContacts { get; set; }
        public List<object> ProjectPartners { get; set; }
        public List<Engagement> Engagements { get; set; }
        public int _lsn { get; set; }
    }
}
