using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TMS.Api.Models;
using TMS.Api.Models.Dto;
using TMS.Api.Repositories;

namespace TMS.Api.Controllers
{
    [Route("api/[Controller][action]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            /*var orderDto = orders.Select(o => new OrderDto()
            {
                OrderId = o.OrderId,
                TicketCategoryId = (int)o.TicketCategoryId,
                CustomerId = (int)o.CustomerId,
                OrderedAt = (DateTime)o.OrderedAt,
                NumberOfTickets = (int)o.NumberOfTickets,
                TotalPrice = (decimal)o.TotalPrice
            });*/
            var orderDto = _mapper.Map<IEnumerable<Order>, List<OrderDto>>(orders);
           
            return Ok(orderDto);
        }


        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var @order =await _orderRepository.GetById(id);

            
            /*var orderDto = new OrderDto()
            {
                OrderId = @order.OrderId,
                TicketCategoryId = (int)@order.TicketCategoryId,
                CustomerId = (int)@order.CustomerId,
                OrderedAt = (DateTime)@order.OrderedAt,
                NumberOfTickets = (int)@order.NumberOfTickets,
                TotalPrice = (decimal)@order.TotalPrice
            };*/
            var orderDto = _mapper.Map<OrderDto>(@order);

            return Ok(orderDto);
        }


        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatch)
        {
            if (orderPatch == null) throw new ArgumentNullException(nameof(orderPatch));
            var orderEntity = await _orderRepository.GetById(orderPatch.OrderId);
            if (orderEntity == null)
            {
                return NotFound();
            }
            if (orderPatch.NumberOfTickets != null) orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            if (orderPatch.TotalPrice != null) orderEntity.TotalPrice = orderPatch.TotalPrice;
            _mapper.Map(orderPatch, orderEntity);
            _orderRepository.Update(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }



    }    

    }

