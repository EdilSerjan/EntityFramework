# EntityFramework
.NET

Lab2 ✅
Lab3 ✅
Lab4 ✅
Lab5 ✅



Milestone Project

Topic: Fitness club. 

7 Entities:

  (1) coach(id, name, tel)
  
  (2) course(id, name, coachId, roomId)
  
  (3) room(room_id, capacity)
  
  (4) equipment(id, name, price, roomId(FK))
  
  (5) member(id, name, tel)
  
  (6) schedule(id, courseId(FK), memberId(FK))
  
  (7) membershipcard(id, createdAt, memberId(FK)) 
 
 relationships:
 
   one to one:
    coach - course
    member - membershipcard

   one to many:
    room - course
    room - equipment

   many to many:
    member - schedule
