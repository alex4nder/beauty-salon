create database beautysalonlocaldb2;
USE beautysalonlocaldb2;

-- Customers Table
CREATE TABLE customers (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    phone VARCHAR(18),
    email VARCHAR(36) UNIQUE,
    birthday DATE
);

-- Services Table
CREATE TABLE services (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    title VARCHAR(36) NOT NULL,
    description VARCHAR(255),
    price DECIMAL(10, 2),
    duration INTEGER
);

-- Employees Table
CREATE TABLE employees (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    first_name VARCHAR(255),
    last_name VARCHAR(255),
    phone VARCHAR(18),
    email VARCHAR(36),
    position VARCHAR(100)
);

-- Appointments Table
CREATE TABLE appointments (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    service_id CHAR(36) NOT NULL,
    employee_id CHAR(36) NOT NULL,
    customer_id CHAR(36) NOT NULL,
    description VARCHAR(255),
    date DATE NOT NULL,
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL,
    status ENUM('created', 'success', 'cancelled') NOT NULL,
    FOREIGN KEY (service_id) REFERENCES services(id) ON DELETE CASCADE,
    FOREIGN KEY (employee_id) REFERENCES employees(id) ON DELETE CASCADE,
    FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
);

-- Schedule Table
CREATE TABLE schedule (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    employee_id CHAR(36) NOT NULL,
    date DATE NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES employees(id) ON DELETE CASCADE
);

-- Добавление сотрудников
INSERT INTO employees (first_name, last_name, email, phone, position) VALUES
('Сергей', 'Кузьмин', 'sergey.kuzmin@example.com', '+7 (495) 111-1111', 'Парикмахер'),
('Марина', 'Воронова', 'marina.voronova@example.com', '+7 (495) 222-2222', 'Стилист'),
('Алексей', 'Трофимов', 'alexey.trofimov@example.com', '+7 (495) 333-3333', 'Колорист'),
('Ольга', 'Медведева', 'olga.medvedeva@example.com', '+7 (495) 444-4444', 'Массажист'),
('Владимир', 'Жуков', 'vladimir.zhukov@example.com', '+7 (495) 555-5555', 'Барбер'),
('Виктория', 'Калашникова', 'victoria.kalashnikova@example.com', '+7 (495) 666-6666', 'Менеджер'),
('Антон', 'Родионов', 'anton.rodionov@example.com', '+7 (495) 777-7777', 'Косметолог');

-- Добавление клиентов
INSERT INTO customers (first_name, last_name, phone, email, birthday) VALUES
('Павел', 'Романов', '+7 (495) 135-7913', 'pavel.romanov@example.com', '1989-01-15'),
('Ксения', 'Фролова', '+7 (495) 246-8024', 'ksenia.frolova@example.com', '1990-05-10'),
('Игорь', 'Морозов', '+7 (495) 357-9135', 'igor.morozov@example.com', '1993-09-20'),
('Алена', 'Павлова', '+7 (495) 468-0246', 'alena.pavlova@example.com', '1992-11-25'),
('Дмитрий', 'Ковалев', '+7 (495) 579-1357', 'dmitry.kovalev@example.com', '1994-12-30'),
('Светлана', 'Николаева', '+7 (495) 681-2468', 'svetlana.nikolaeva@example.com', '1995-06-12'),
('Артем', 'Егоров', '+7 (495) 792-3579', 'artem.egorov@example.com', '1996-04-22');

-- Добавление услуг
INSERT INTO services (title, description, price, duration) VALUES
('Антистресс-массаж', 'Глубокий массаж для снятия стресса и усталости.', 10000.00, 60),
('Биозавивка', 'Завивка волос без химии, с использованием натуральных средств.', 12000.00, 90),
('Окрашивание корней', 'Осветление и окрашивание корней волос.', 5000.00, 45),
('Спа-педикюр', 'Оздоровительный педикюр с аромамаслами.', 4000.00, 60),
('Аромамассаж', 'Массаж с использованием ароматических масел.', 9000.00, 60),
('Пилинг лица', 'Глубокий очищающий пилинг.', 4500.00, 45),
('Макияж для фотосессии', 'Профессиональный макияж для фотосессий.', 5500.00, 60),
('Стрижка', 'Обычная стрижка для мужчин и женщин.', 2000.00, 30);


-- Добавление записей на прием
INSERT INTO appointments (customer_id, service_id, employee_id, description, date, start_time, end_time, status) VALUES
((SELECT id FROM customers WHERE email = 'pavel.romanov@example.com'), (SELECT id FROM services WHERE title = 'Антистресс-массаж'), (SELECT id FROM employees WHERE email = 'sergey.kuzmin@example.com'), 'Запись на антистресс-массаж', '2024-10-24', '2024-10-24 12:00:00', '2024-10-24 13:00:00', 'success'),
((SELECT id FROM customers WHERE email = 'ksenia.frolova@example.com'), (SELECT id FROM services WHERE title = 'Биозавивка'), (SELECT id FROM employees WHERE email = 'sergey.kuzmin@example.com'), 'Запись на биозавивку', '2024-10-25', '2024-10-25 10:00:00', '2024-10-25 11:30:00', 'created'),
((SELECT id FROM customers WHERE email = 'igor.morozov@example.com'), (SELECT id FROM services WHERE title = 'Окрашивание корней'), (SELECT id FROM employees WHERE email = 'sergey.kuzmin@example.com'), 'Запись на окрашивание корней', '2024-10-26', '2024-10-26 14:00:00', '2024-10-26 14:45:00', 'created'),
((SELECT id FROM customers WHERE email = 'alena.pavlova@example.com'), (SELECT id FROM services WHERE title = 'Спа-педикюр'), (SELECT id FROM employees WHERE email = 'sergey.kuzmin@example.com'), 'Запись на спа-педикюр', '2024-10-27', '2024-10-27 16:00:00', '2024-10-27 17:00:00', 'cancelled'),
((SELECT id FROM customers WHERE email = 'dmitry.kovalev@example.com'), (SELECT id FROM services WHERE title = 'Аромамассаж'), (SELECT id FROM employees WHERE email = 'marina.voronova@example.com'), 'Запись на аромамассаж', '2024-10-28', '2024-10-28 11:00:00', '2024-10-28 12:00:00', 'success'),
((SELECT id FROM customers WHERE email = 'svetlana.nikolaeva@example.com'), (SELECT id FROM services WHERE title = 'Пилинг лица'), (SELECT id FROM employees WHERE email = 'marina.voronova@example.com'), 'Запись на пилинг лица', '2024-10-29', '2024-10-29 13:00:00', '2024-10-29 13:45:00', 'created'),
((SELECT id FROM customers WHERE email = 'artem.egorov@example.com'), (SELECT id FROM services WHERE title = 'Макияж для фотосессии'), (SELECT id FROM employees WHERE email = 'marina.voronova@example.com'), 'Запись на макияж для фотосессии', '2024-10-30', '2024-10-30 15:00:00', '2024-10-30 16:00:00', 'created');

-- Добавление расписания
INSERT INTO schedule (employee_id, date, start_time, end_time) VALUES
((SELECT id FROM employees WHERE email = 'sergey.kuzmin@example.com'), '2024-10-16', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'marina.voronova@example.com'), '2024-10-17', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'alexey.trofimov@example.com'), '2024-10-18', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'olga.medvedeva@example.com'), '2024-10-19', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'vladimir.zhukov@example.com'), '2024-10-20', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'victoria.kalashnikova@example.com'), '2024-10-21', '09:00:00', '17:00:00'),
((SELECT id FROM employees WHERE email = 'anton.rodionov@example.com'), '2024-10-22', '10:00:00', '18:00:00'),
((SELECT id FROM employees WHERE email = 'sergey.kuzmin@example.com'), '2024-10-23', '08:00:00', '16:00:00');
