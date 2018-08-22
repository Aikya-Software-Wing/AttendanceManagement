CREATE TABLE Department (
  DID nvarchar(4) PRIMARY KEY,
  Name nvarchar(50) NOT NULL,
);

CREATE TABLE Teacher (
  TID nvarchar(7) PRIMARY KEY,
  Name nvarchar(50) NOT NULL,
  Department_DID nvarchar(4),
  FOREIGN KEY(Department_DID) references Department(DID) 
  ON DELETE SET NULL
);

CREATE TABLE Subject (
  SubCode nvarchar(7) PRIMARY KEY,
  Name nvarchar(50) NOT NULL,
  Department_DID nvarchar(4),
 FOREIGN KEY(Department_DID) references Department(DID) 
 ON DELETE SET NULL
);

CREATE TABLE Student (
   USN nvarchar(10) PRIMARY KEY,
   Name nvarchar(50) NOT NULL,
   Section nchar(1) NOT NULL,
   Sem integer NOT NULL,
   Department_DID nvarchar(4),
   Subject_SubCode nvarchar(7) NOT NULL,
  FOREIGN KEY(Subject_SubCode) references Subject(SubCode),
  FOREIGN KEY(Department_DID) references Department(DID) 
  ON DELETE CASCADE
  ON UPDATE CASCADE
);

CREATE TABLE Attendance (
  Student_USN nvarchar(10),
  Teacher_TID nvarchar(7),
  Subject_SubCode nvarchar(7),
  Date date,
  Slot integer,
  Status nchar(1) NOT NULL,
  PRIMARY KEY (Date, Slot, Student_USN, Teacher_TID, Subject_SubCode),
  FOREIGN KEY(Subject_SubCode) references Subject(SubCode),
  FOREIGN KEY(Student_USN) references Student(USN), 
  FOREIGN KEY(Teacher_TID) references Teacher(TID) 
);

CREATE TABLE Teacher_Teaches_Student (
  Subject_SubCode nvarchar(7),
  Student_USN nvarchar(10),
  Teacher_TID nvarchar(7),
  PRIMARY KEY(Student_USN, Teacher_TID, Subject_SubCode),
  FOREIGN KEY (Subject_SubCode) references Subject(SubCode),
  FOREIGN KEY (Student_USN) references Student(USN),
  FOREIGN KEY (Teacher_TID) references Teacher(TID)
);

