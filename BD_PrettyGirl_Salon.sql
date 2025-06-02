-- Creación de la base de datos
CREATE DATABASE PrettyGirl_Salon;
GO

USE PrettyGirl_Salon;
GO

-- Tabla Cliente 
CREATE TABLE client (
  id int IDENTITY(1,1) PRIMARY KEY,
  dni varchar(8) UNIQUE NOT NULL,
  fullName varchar(255) NOT NULL,
  phone varchar(15) NOT NULL,
  email varchar(100) NULL,
  registrationDate DATE DEFAULT GETDATE() NOT NULL
);

-- Tabla Estilista 
CREATE TABLE stylist (
  userName varchar(50) PRIMARY KEY,
  userPassword varchar(255) NOT NULL,
  fullName varchar(255) NOT NULL,
  specialty varchar(100) NOT NULL,
  email varchar(255) UNIQUE NOT NULL,
  isActive bit DEFAULT 1 NOT NULL --Se añadió para identificar los estilistas disponibles, por si alguno sale de vacaciones
);

-- Tabla Servicio 
CREATE TABLE serviceRequest (
  id int IDENTITY(1,1) PRIMARY KEY,
  serviceName varchar(255) NOT NULL,
  durationMinutes int NOT NULL,
  servicePrice decimal(10,2) NOT NULL,
  serviceDescription varchar(500) NULL,  
  isAvailable BIT DEFAULT 1 NOT NULL -- Se añadió para identificar los servicios disponibles
);

-- Tabla Cita 
CREATE TABLE appointment (
  id int IDENTITY(1,1) PRIMARY KEY,
  clientId int NOT NULL,
  serviceId int NOT NULL,
  stylistUser varchar(50) NOT NULL,
  appointmentDateTime DATETIME NOT NULL,
  appointmentStatus varchar(20) DEFAULT 'pending' NOT NULL, -- pending, confirmed, completed, cancelled
  notes varchar(1000), -- Para anotaciones no contempladas en los servicios, o anotar alguna incidencia
  FOREIGN KEY (clientId) REFERENCES client(id),
  FOREIGN KEY (serviceId) REFERENCES serviceRequest(id),
  FOREIGN KEY (stylistUser) REFERENCES stylist(userName),
  INDEX idx_appointment_datetime (appointmentDateTime)
);

-- Tabla Valoracion 
CREATE TABLE review (
  id int IDENTITY(1,1) PRIMARY KEY,
  appointmentId int NOT NULL,
  ratingValue int NOT NULL, -- de 1 a 5
  reviewComment varchar(500),
  reviewDate DATE DEFAULT GETDATE() NOT NULL,
  response VARCHAR(500), --Para responder a las valoraciones (agradecimientos, aclaraciones, etc)
  FOREIGN KEY (appointmentId) REFERENCES appointment(id),
  CHECK (ratingValue BETWEEN 1 AND 5),
  INDEX idx_review_date (reviewDate)
);
