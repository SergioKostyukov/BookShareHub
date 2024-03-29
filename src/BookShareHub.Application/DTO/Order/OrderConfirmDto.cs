﻿namespace BookShareHub.Application.Dto.Order;

public record OrderConfirmDto(
	int OrderId,
	string OwnerId,
	string OwnerName,
	string Comment,
	string DeliveryAddress,
	string DeliveryUserFullName,
	string DeliveryUserPhone
);
