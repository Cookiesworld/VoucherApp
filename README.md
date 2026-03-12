# VoucherApp

Take up to 30 minutes to complete the following scenario. Talk through your thought
process as you work and feel free to ask questions if anything is unclear.
## Scenario:
You're developing an online platform for an insurance company where customers
can purchase various insurance policies and benefit from discount vouchers. Your
task is to implement a class OrderProcessor with a method PlaceOrder that handles
order creation and discount application.
Included are some classes to start you off, but feel free to add and update classes as
you see fit. The interface for IOrderProcessor has been provided for you to
implement.
## Tips:
* Due to the time constraints, please abstract away any data stores but feel free
to discuss how you think these might tie into your design.
* Don’t worry if you cannot complete the whole thing, it is intended to be
enough to take us beyond the time limit.

## Technical Question:
Implement the PlaceOrder method within the OrderProcessor class to take a Policy
and an optional Voucher as input and return a new Order. The PlaceOrder method
should fulfil the following functionalities:
1. Create an Order
   * Create and return an Order with details of the purchase and which
policy it is for.
   * If a voucher is provided and is valid, calculate the appropriate discount
amount using the voucher.
   * Ensure the final price after discount remains non-negative.
1. Redeem the voucher
   * If a voucher is provided, it will need to be redeemed. Each voucher cannot be used multiple times, for the same policy or any other policy
   * Please keep in mind that in the future, the company would like to integrate with external voucher providers which will run alongside vouchers offered directly from the company.
1. Persist order data in a database.
2. You do not need to implement an actual database for this, it can be interfaced / mocked out.
* Handle error conditions gracefully with informative messages to the user.
* The user will need to know for example if the voucher is invalid or has
already been used.
## Bonus:
* Think about applying multiple vouchers to an order if applicable and how this
might work.