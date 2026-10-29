DROP DATABASE IF EXISTS devworkbench;

DROP TABLE IF EXISTS USER;
DROP TABLE IF EXISTS JWT;

CREATE TABLE USER (
    userid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    userpassword VARCHAR(255) NOT NULL, 
    email TEXT UNIQUE NOT NULL,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    isactive BIT NOT NULL DEFAULT 0,
    roles VARCHAR(10) not null DEFAULT "user"
);

CREATE TABLE JWT (
    jwtid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    userid bigint not null,
    token TEXT not null,
    token_type VARCHAR(10) NOT NULL,-- Logout, Password Change
    created_at TIMESTAMPTZ DEFAULT NOW(),
    CONSTRAINT fk_user_jwt
        FOREIGN KEY (userid)
        REFERENCES user(userid)
        ON DELETE CASCADE
);