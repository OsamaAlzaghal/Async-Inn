# Async-Inn
---
## Name: Osama Alzaghal.
## Date: 14-4-2022.
---

### Introduction
This web app is for a hotel management system. Users can view the hotels, rooms, and the amenities available either by retrieving all data of each table or by getting a specific one using it's ID.
### I have the following tables:
+ Room: so this room has a unique ID and other properites such as name and layout.
+ Hotel: the hotel has an ID, name, street address, city, state, country, and phone. All of these are provided in the requierments.
+ Amenities: it has an ID and a name. It navigates to RoomAmenities table since the relationship between them is a many to many.
+ RoomAmenities: It has two CK and FK; because this table is responsible of connecting both of Room and Amenities tables, and it navigates to Room and Amenities tables; because relationship between them is a many to many..
+ HotelRoom: it has An ID, CK and and FK, along with other properties such as Rate and PetFriendly. All of these were mentioned in the requierments. It navigates to Hotel and Room table; because the relationship between them is a many to many.
+ Enum for the size of Room, it could be a studio or one/two bedroom(s).
---
## Async Inn ERD
![ERD](AsyncInn.drawio.png)