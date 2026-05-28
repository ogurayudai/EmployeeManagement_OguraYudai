DROP TABLE IF EXISTS login;
DROP TABLE IF EXISTS employee;
DROP TABLE IF EXISTS department;

CREATE TABLE department (
    id SERIAL PRIMARY KEY,
    dept_name VARCHAR(50) NOT NULL
);

CREATE TABLE employee (
    id SERIAL PRIMARY KEY,
    department_id INT NOT NULL,
    employee_no VARCHAR(10) NOT NULL UNIQUE,
    name VARCHAR(50) NOT NULL,
    name_kana VARCHAR(50) NOT NULL,
    email_address VARCHAR(100) NOT NULL,
    birthday DATE NOT NULL,
    gender INT NOT NULL,
    FOREIGN KEY (department_id) REFERENCES department(id)
);

CREATE TABLE login (
    id SERIAL PRIMARY KEY,
    email_address VARCHAR(100) NOT NULL,
    employee_id INT NOT NULL,
    login_password VARCHAR(255) NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES employee(id)
);

-- =========================
-- テストデータ
-- =========================

INSERT INTO department (dept_name)
VALUES ('営業');

INSERT INTO employee (
    department_id,
    employee_no,
    name,
    name_kana,
    email_address,
    birthday,
    gender
)
VALUES (
    1,
    '200801',
    '山田太郎',
    'やまだたろう',
    'test@example.com',
    '2000-01-01',
    1
);

INSERT INTO login (
    email_address,
    employee_id,
    login_password
)
VALUES (
    'test@example.com',
    1,
    'password'
);