create database learningPlus

use learningPlus
go
create table roles(
id int primary key,
name varchar(20) not null
)
insert into roles(id, name) values
(1, 'admin'),
(2, 'instructor'),
(3, 'student')
go
select * from roles
create table users(
userId int primary key identity(1,1),
firstName varchar(20) not null,
lastName varchar(20) not null,
email varchar(50) not null,
mobileNumber numeric(10) not null,
profilePhoto varchar(200),
gender char(1) check(gender in('M','F')),
password varchar(20) not null,
roleId int,
membershipStatus varchar(20) check (membershipStatus in ('active','inactive')) default 'inactive',
membership_expiryDate date,
created_at datetime default GETDATE(),
updated_at datetime default GETDATE(),
constraint fk_Role foreign key(roleId) references roles(id) on delete set null
)
go
insert into users values('harry','potter','harry@gamil.com',1234567890,'/','M','Harry@123',3,'active','2024-12-31',GETDATE(),GETDATE())
insert into users values('hermione','granger','hermione.gamil.com',1234567891,'/','F','Hermione@123',3,'active','2024-12-31',GETDATE(),GETDATE())
insert into users values('ron','weasley','ron.gamil.com',1234567892,'/','M','Ron@123',3,'active','2024-12-31',GETDATE(),GETDATE())
insert into users values('albus','dumbledore','albus.gamil.com',1234567893,'/','M','Albus@123',1,'active','2024-12-31',GETDATE(),GETDATE())
insert into users values('severus','snape','severus.gamil.com',1234567894,'/','M','Severus@123',2,'active','2024-12-31',GETDATE(),GETDATE())
insert into users values('minerva','mcgonagall','minerva.gamil.com',1234567895,'/','F','Minerva@123',2,'active','2024-12-31',GETDATE(),GETDATE())
insert into users values('sirius','black','sirius.gamil.com',1234567896,'/','M','Sirius@123',2,'active','2024-12-31',GETDATE(),GETDATE())
select * from users

create table courses(
courseId varchar(20) primary key,
courseName varchar(50) not null,
)
alter table courses add description varchar(200) not null
alter table courses add courseTpye varchar(20) check (courseTpye in ('free','paid')) default 'free'
alter table courses add price decimal(10,2) default 0.00 -- Price for paid courses
create table courseContent(
id int primary key identity(1,1),
courseId varchar(20) not null,
title varchar(50) not null,
description varchar(200) not null,
contentUrl varchar(200) not null,
sequence int not null,
isFree bit not null default 0,
notesUrl varchar(2000),
CONSTRAINT fk_courseContent_course FOREIGN KEY (courseId) REFERENCES courses(courseId) ON DELETE CASCADE
)
GO
create table courseEnrollments(
enrollmentId int primary key identity(1,1),

userId int not null,
courseId varchar(20) not null,
enrollmentDate datetime default GETDATE(),
validUntil date,
status varchar(20) check (status in ('active','completed','dropped')) default 'active',
constraint fk_courseEnrollment_user foreign key(userId) references users(userId) on delete cascade,
constraint fk_courseEnrollment_course foreign key(courseId) references courses(courseId) on delete cascade
)
GO
create table courseProgress(
id int primary key identity(1,1),
userId int not null,
courseId varchar(20) not null,
isCompleted bit not null default 0,
progressPercentage int not null default 0,
lastAccessed datetime default GETDATE(),
constraint fk_courseProgress_user foreign key(userId) references users(userId) on delete cascade,
constraint fk_courseProgress_course foreign key(courseId) references courses(courseId) on delete cascade
)
go
CREATE TABLE courseContentProgress (
    contentId INT NOT NULL,
    userId INT NOT NULL,
    courseId VARCHAR(20) NOT NULL,
    isCompleted BIT NOT NULL DEFAULT 0,
    progressPercentage INT NOT NULL DEFAULT 0,
    lastAccessed DATETIME DEFAULT GETDATE(),
    CONSTRAINT pk_courseContentProgress PRIMARY KEY (userId, contentId),
    CONSTRAINT fk_courseContentProgress_content 
        FOREIGN KEY (contentId) 
        REFERENCES courseContent(id) 
        ON DELETE CASCADE,
    CONSTRAINT fk_courseContentProgress_user 
        FOREIGN KEY (userId) 
        REFERENCES users(userId) 
        ON DELETE CASCADE,
    CONSTRAINT fk_courseContentProgress_course 
        FOREIGN KEY (courseId) 
        REFERENCES courses(courseId) 
        ON DELETE NO ACTION
)
GO
create table Payments(
    paymentId int primary key identity(1,1),
    userId int not null,
    courseId varchar(20) not null,
    amount decimal(10,2) not null,
    paymentDate datetime default GETDATE(),
    paymentStatus varchar(20) check (paymentStatus in ('pending', 'completed', 'failed')) default 'pending',
    constraint fk_payment_user foreign key(userId) references users(userId) on delete cascade,
    constraint fk_payment_course foreign key(courseId) references courses(courseId) on delete cascade
)
go
create table assessment(
assessmentId int primary key identity(1,1),
courseId varchar(20) not null,
title varchar(50) not null,
noOfQuestions int not null,
totalMarks int not null,
passingMarks int not null,
duration int not null, -- in minutes
constraint fk_assessment_course foreign key(courseId) references courses(courseId) on delete cascade
)
go
create table assessmentQuestions(
questionId int primary key identity(1,1),
assessmentId int not null,
questionText varchar(500) not null,
marks int not null,
)
alter table assessmentQuestions add constraint
fk_assessmentQuestions_assessment foreign key(assessmentId) 
references assessment(assessmentId) on delete cascade
go
create table assessmentAnswers(
answerId int primary key identity(1,1),
questionId int not null,
optionText varchar(200) not null,
isCorrect bit not null default 0,
constraint fk_ques foreign key(questionId) references assessmentQuestions(questionId) on delete cascade
)
go
create table userAssessmentResults(
assessmentResultId int primary key identity(1,1),
userId int not null,
courseId varchar(20) not null,
assessmentId int not null,
marksObtained int,
Userstatus varchar(20) check(Userstatus in('passed','failed','not attempted')) default 'not attemped',
attemptDate datetime,
certificateIssued bit default 0,
CONSTRAINT us_userId foreign key(userId) references users(userId) on delete cascade,
constraint us_courseId foreign key(courseId) references courses(courseId) on delete cascade,
constraint us_assesmentId foreign key(assessmentId) references assessment(assessmentId) on delete no action
)
go
create table certificates(
certificateId int primary key identity(1,1),
userId int not null,
courseId varchar(20) not null,
assessmentId int not null,
certificateUrl varchar(200) not null,
issueDate datetime default GETDATE(),
percentageObtained int not null, -- Percentage obtained in the assessment
issuedBy varchar(50) not null, -- Name of the person or organization issuing the certificate
constraint fk_certificate_user foreign key(userId) references users(userId) on delete cascade,
constraint fk_certificate_course foreign key(courseId) references courses(courseId) on delete cascade,
constraint fk_certificate_assessment foreign key(assessmentId) references assessment(assessmentId) on delete no action
)


go
create table UserBankDetails(
    bankId int primary key identity(1,1),
    userId int not null,
    accountNumber varchar(20) not null unique,
    accountHolderName varchar(50) not null,
    bankName varchar(50) not null,
    ifscCode varchar(20) not null,
    isDefault bit default 0, -- Indicates if this is the default bank account for the user
    availableBalance decimal(10,2) default 0.00, -- Balance available in the bank account
    constraint fk_bank_user foreign key(userId) references users(userId) on delete cascade
)

create table saveCardDetails(
    cardId int primary key identity(1,1),
    userId int not null,
    accountNumber varchar(20) not null unique, -- Unique account number for the card
    cardNumber varchar(20) not null,
    cardHolderName varchar(50) not null,
    expiryDate date not null,
    cvv int not null,
    isDefault bit default 0, -- Indicates if this is the default card for the user
    created_at datetime default GETDATE(),
    updated_at datetime default GETDATE(),
    constraint fk_card_user foreign key(userId) references users(userId) on delete cascade
)
create table Coursefeedbacks(
    feedbackId int primary key identity(1,1),
    userId int not null,
    courseId varchar(20) not null,
    rating int check(rating between 1 and 5), -- Rating from 1 to 5
    comment varchar(500), -- Optional comment
    feedbackDate datetime default GETDATE(),
    constraint fk_feedback_user foreign key(userId) references users(userId) on delete cascade,
    constraint fk_feedback_course foreign key(courseId) references courses(courseId) on delete cascade
)
go
create table contentFeedbacks(
    feedbackId int primary key identity(1,1),
    userId int not null,
    contentId int not null,
    rating int check(rating between 1 and 5), -- Rating from 1 to 5
    comment varchar(500), -- Optional comment
    feedbackDate datetime default GETDATE(),
    constraint fk_contentFeedback_user foreign key(userId) references users(userId) on delete cascade,
    constraint fk_contentFeedback_content foreign key(contentId) references courseContent(id) on delete cascade
)
go
create table userDoubts(
    doubtId int primary key identity(1,1),
    userId int not null,
    courseId varchar(20) not null,
    contentId int not null,
    doubtText varchar(500) not null, -- Text of the doubt
    status varchar(20) check(status in ('pending', 'resolved', 'closed')) default 'pending', -- Status of the doubt
    created_at datetime default GETDATE(),
    updated_at datetime default GETDATE(),
    constraint fk_doubt_user foreign key(userId) references users(userId) on delete cascade,
    constraint fk_doubt_course foreign key(courseId) references courses(courseId) on delete cascade,
    constraint fk_doubt_content foreign key(contentId) references courseContent(id) on delete no action
)
create table doubtResponses(
    responseId int primary key identity(1,1),
    doubtId int not null,
    userId int not null, -- User who is responding to the doubt
    responseText varchar(500) not null, -- Text of the response
    created_at datetime default GETDATE(),
    updated_at datetime default GETDATE(),
    constraint fk_response_doubt foreign key(doubtId) references userDoubts(doubtId) on delete cascade,
    constraint fk_response_user foreign key(userId) references users(userId) on delete no action
)
select * from users
create table userNotifications(
    notificationId int primary key identity(1,1),
    userId int not null,
    message varchar(500) not null, -- Notification message
    isRead bit default 0, -- Indicates if the notification has been read
    created_at datetime default GETDATE(),
    updated_at datetime default GETDATE(),
    constraint fk_notification_user foreign key(userId) references users(userId) on delete cascade
)


select * from users
go
-- adding stored procefure for user registration and login--
CREATE PROCEDURE [dbo].[usp_userRegistration]
(
    @firstName     VARCHAR(20),
    @lastName      VARCHAR(20),
    @email         VARCHAR(50),
    @mobileNumber  NUMERIC(10),
    @gender        CHAR(1),   
    @password      VARCHAR(20),
    @userID        INT OUT,     -- Output parameter for user ID
    @roleId        INT OUT      -- Output parameter for role ID
)
AS
BEGIN
    DECLARE @returnValue INT
    SET @userID = 0
    SET @roleId = 3 -- Default role is student (3)

    BEGIN TRY
        -- Input validation
        IF (
            @firstName IS NULL OR
            @lastName IS NULL OR
            @email IS NULL OR
            @mobileNumber IS NULL OR
            @gender IS NULL OR
            @password IS NULL
        )
        BEGIN
            SET @returnValue = -1 -- Invalid input
            RETURN @returnValue
        END

        -- Check if email already exists
        IF EXISTS (SELECT 1 FROM users WHERE email = @email)
        BEGIN
            SET @returnValue = -2 -- Email already exists
            RETURN @returnValue
        END

        -- Check if mobile number already exists
        IF EXISTS (SELECT 1 FROM users WHERE mobileNumber = @mobileNumber)
        BEGIN
            SET @returnValue = -3 -- Mobile number already exists
            RETURN @returnValue
        END

        -- Insert user into table, including roleId
        INSERT INTO users (firstName, lastName, email, mobileNumber, gender, password, roleId)
        VALUES (@firstName, @lastName, @email, @mobileNumber, @gender, @password, @roleId)

        -- Get newly inserted user ID
        SET @userID = SCOPE_IDENTITY()
        SET @returnValue = 1 -- Success
    END TRY
    BEGIN CATCH
        SET @userID = 0
        SET @roleId = 0
        SET @returnValue = -99 -- General error
    END CATCH

    RETURN @returnValue
END
GO
--stored prpcedure f user login--
CREATE PROCEDURE usp_userLogin
(
    @email VARCHAR(50),
    @mobileNumber NUMERIC(10),
    @password VARCHAR(20),
    @userId INT OUT,
    @roleId INT OUT
)
AS
BEGIN
    DECLARE @returnValue INT
    SET @userId = 0
    SET @roleId = 0

    BEGIN TRY
        -- Check if both email and phone are null
        IF (@email IS NULL AND @mobileNumber IS NULL)
        BEGIN
            SET @returnValue = -1 -- Invalid input: both missing
            RETURN @returnValue
        END

        -- Fetch user by email OR mobile with password
        SELECT @userId = userId, @roleId = roleId
        FROM users
        WHERE 
            (@email IS NOT NULL AND email = @email OR
             @mobileNumber IS NOT NULL AND mobileNumber = @mobileNumber)
            AND password = @password

        -- Check if user was found
        IF (@userId IS NULL OR @userId = 0)
        BEGIN
            SET @returnValue = -2 -- User not found or invalid credentials
            RETURN @returnValue
        END

        SET @returnValue = 1 -- Success
    END TRY
    BEGIN CATCH
        SET @userId = 0
        SET @roleId = 0
        SET @returnValue = -99 -- General error
    END CATCH

    RETURN @returnValue
END
GO
