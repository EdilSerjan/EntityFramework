# Milestone Project

### Topic: Fitness club. 

Nowadays, **Fitness club** is becoming more and more popular. So I decided to develop the application about a fitness club. I have 7 entities, in which there are three types of relaitonships as well.





### 7 Entities:

  - coach(id, name, tel)
  
  - course(id, name, coachId, roomId)
  
  - room(room_id, capacity)
  
  - equipment(id, name, price, roomId(FK))
  
  - member(id, name, tel)
  
  - schedule(id, courseId(FK), memberId(FK))
  
  - membershipcard(id, createdAt, memberId(FK)) 
 
 
### Relationships:
 
  - one to one:
    - coach - course
    - member - membershipcard

  - one to many:
    - room - course
    - room - equipment

  - many to many:
    - member - course
