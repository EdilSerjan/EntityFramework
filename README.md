# Milestone Project

### Serzhan Yedil CSSE-1601K 25000

### Topic: Fitness club. 


Nowadays, **Fitness club** is becoming more and more popular. So I decided to develop the application about a fitness club. I have 7 entities, in which there are three types of relaitonships as well.





### 7 Entities:

  - coach(id, name, tel)
  
  - course(id, name, coachId, roomId)
  
  - room(room_id, capacity)
  
  - equipment(id, name, price, roomId(FK))
  
  - member(id, name, tel)
  
  - couresmember(id, courseId(FK), memberId(FK))
  
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

### Validations

#### the remote validation
  
VerifyEmail of the property '**Email**' of model '**Member**' and the function is in the Controller '**MembersController**'
      
#### the custom attriubute validation
  
The custom attribute validation is '**NotContainsDigits**'
It validates the property '**Name**' of model '**Member**', making sure that the name contains no digits,
the implementation is in the same model.
  
#### the model which implements IValidatableObject
  
The model '**Equipment**' inherits **IValidatableObject**
And we'r using it to make sure when the equipment's name is 'bench', its price should not exceed 20000.
  
       
