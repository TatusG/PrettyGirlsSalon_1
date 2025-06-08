use PrettyGirl_Salon;

-- Insertar estilistas (stylist)
INSERT INTO stylist (userName, userPassword, fullName, specialty, email, isActive) 
VALUES 
('maria', '1234', 'María López', 'Coloración', 'maria@prettygirlbs.com', 1),
('juan', '567', 'Juan Pérez', 'Cortes masculinos', 'juan@prettygirlbs.com', 1),
('laura', '891', 'Laura García', 'Tratamientos capilares', 'laura@prettygirlbs.com', 1);

-- Insertar servicios (serviceRequest)
INSERT INTO serviceRequest (serviceName, durationMinutes, servicePrice, serviceDescription, isAvailable)
VALUES
('Corte de cabello', 30, 25.00, 'Corte y peinado básico', 1),
('Coloración completa', 120, 80.00, 'Tinte completo con productos profesionales', 1),
('Mechas', 90, 65.00, 'Aplicación de mechas californianas', 1),
('Tratamiento keratina', 60, 70.00, 'Tratamiento reparador con keratina', 1),
('Corte infantil', 20, 15.00, 'Corte especial para niños', 1);

-- Insertar clientes (client)
INSERT INTO client (dni, fullName, phone, email, registrationDate)
VALUES
('40587452', 'Ana Martínez', '985652474', 'ana@gmail.com', GETDATE()),
('19165230', 'Carlos Ruiz', '975142365', 'carlos@hotmail.com', GETDATE()),
('20568794', 'Sofía García', '965124785', 'sofia@gmail.com', GETDATE()),
('50874512', 'David López', '932145874', 'david@yahoo.com', GETDATE());

-- Insertar citas (appointment) con notas adicionales
INSERT INTO appointment (clientId, serviceId, stylistUser, appointmentDateTime, appointmentStatus, notes)
VALUES
(1, 2, 'maria', '2025-05-15 10:00:00', 'completed', 'Cliente prefiere tintura sin amoníaco'),
(2, 1, 'juan', '2025-05-30 11:30:00', 'confirmed', 'Quiere el mismo corte que la última vez'),
(3, 3, 'laura', '2025-06-16 16:00:00', 'pending', NULL),
(4, 5, 'maria', '2025-06-20 09:00:00', 'confirmed', 'Niño de 5 años, primera visita');

-- Insertar valoraciones (review) con respuestas
INSERT INTO review (appointmentId, ratingValue, reviewComment, reviewDate, response)
VALUES
(1, 5, 'Excelente servicio, muy profesional', GETDATE(), '¡Gracias Ana! Nos alegra que hayas disfrutado tu experiencia'),
(2, 4, 'Buen trabajo pero un poco lento', GETDATE(), 'Gracias por tu feedback Carlos, estamos mejorando nuestros tiempos');

-- Consulta para ver citas pendientes
SELECT 
    a.id, 
    c.fullName AS client, 
    s.serviceName AS service, 
    st.fullName AS stylist, 
    a.appointmentDateTime, 
    a.appointmentStatus,
    a.notes
FROM appointment a
JOIN client c ON a.clientId = c.id
JOIN serviceRequest s ON a.serviceId = s.id
JOIN stylist st ON a.stylistUser = st.userName
WHERE a.appointmentStatus = 'pending'
ORDER BY a.appointmentDateTime;

-- Consulta para agenda diaria de un estilista
SELECT 
    a.appointmentDateTime, 
    c.fullName AS client, 
    s.serviceName AS service, 
    s.durationMinutes,
    a.notes
FROM appointment a
JOIN client c ON a.clientId = c.id
JOIN serviceRequest s ON a.serviceId = s.id
WHERE a.stylistUser = 'maria'
  AND CONVERT(DATE, a.appointmentDateTime) = '2025-05-15'
ORDER BY a.appointmentDateTime;

-- Consulta de valoraciones por estilista con promedio
SELECT 
    s.fullName AS stylist, 
    AVG(r.ratingValue) AS averageRating,
    COUNT(r.id) AS totalReviews
FROM review r
JOIN appointment a ON r.appointmentId = a.id
JOIN stylist s ON a.stylistUser = s.userName
GROUP BY s.fullName
ORDER BY averageRating DESC;

select * from client;
select * from appointment;
select * from serviceRequest;
select * from stylist;

-- Consulta Serivicios por cliente
SELECT 
    c.id AS 'ID Cliente',
    c.fullName AS 'Nombre Cliente',
    c.phone AS 'Teléfono',
    c.email AS 'Email',
    s.serviceName AS 'Servicio',
    s.servicePrice AS 'Precio',
    s.durationMinutes AS 'Duración (min)',
    st.fullName AS 'Estilista',
    a.appointmentDateTime AS 'Fecha y Hora',
    a.appointmentStatus AS 'Estado'
FROM 
    client c
JOIN 
    appointment a ON c.id = a.clientId
JOIN 
    serviceRequest s ON a.serviceId = s.id
JOIN 
    stylist st ON a.stylistUser = st.userName
WHERE 
    a.appointmentStatus = 'completed'
ORDER BY 
    a.appointmentDateTime DESC;

--Seleccionar Estilista por servicio
select st.fullName, s.serviceName, st.specialty
from serviceRequest s
join appointment a on s.id = a.serviceId
join stylist st on a.stylistUser = st.userName;

	select * from client;
	select * from appointment;
	select * from review;
	select * from serviceRequest;
la

	select s.userName
	from stylist s where s.userName = 'tatus' and s.userPassword = '789';