# CatMash is pedagogic project which aims to refact a Transaction script design to a Domain model design

Architecture used is a Layered architecture

This solution contains both "Transaction script" and "Domain driven" designs
Only Domain layers are specific to one design
Others are commons for both.

Transaction script domain layers : 
- CatMash.Business
- CatMash.Domain

Domain model layers : 
- CatMash.Core


Inside the API, a "CatDddController" is refactored in order to use Domain model logic, whereas "CatController" use the transaction script oriented logic
