using System.Threading.Tasks;
using App.CheckIn.Domain.Repositories;
using AppCheckInSubscribe.Application;
using AppCheckInSubscribe.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;
using Tnf.Runtime.Session;

namespace AppCheckInSubscribe.Controllers.Web
{
    /// <summary>
    /// Controller responsible for handling request of the subscription route
    /// </summary>
    [Route("api/subscription")]
    [Produces("application/json")]
    [TnfAuthorize(Permission = Permissions.Subscription, Description = Permissions.SubscriptionDescription)]
    [ApiController]
    public class SubscriptionController : TnfController
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IEventSubscriptionRepository _repository;
        private readonly ITnfSession _session;

        public SubscriptionController(
            ISubscriptionService subscriptionService,
            IEventSubscriptionRepository repository,
            ITnfSession session)
        {
            _subscriptionService = subscriptionService;
            _repository = repository;
            _session = session;
        }

        /// <summary>
        /// Gets all subscription of the logged user
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ListDto<EventSubscriptionResponse>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery] RequestAllDto requestAllDto)
        {
            if (!_session.UserId.HasValue)
            {
                return BadRequest();
            }

            var userId = _session.UserId.GetValueOrDefault();

            var subscriptions = await _repository.GetUserSubscription(userId, requestAllDto);

            var response = subscriptions.Map(subscription => new EventSubscriptionResponse(subscription));

            return CreateResponseOnGetAll(response);
        }

        /// <summary>
        /// Saves a subscription for the logged user
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SubscribeToEventResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> PostAsync([FromBody] SubscribeToEventRequest subscriptionDto)
        {
            if (subscriptionDto == null)
            {
                return BadRequest();
            }

            if (!_session.UserId.HasValue)
            {
                return BadRequest();
            }

            var userId = _session.UserId.GetValueOrDefault();

            var subscription = await _subscriptionService.SubscribeAsync(subscriptionDto, userId);

            return CreateResponseOnPost(subscription);
        }
    }
}
