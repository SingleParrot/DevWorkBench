DROP DATABASE IF EXISTS devworkbench;

DROP TABLE IF EXISTS "User";
DROP TABLE IF EXISTS "Jwt";

CREATE TABLE "User" (
    userid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    userpassword VARCHAR(255) NOT NULL,
    email TEXT UNIQUE NOT NULL,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    isactive BOOLEAN NOT NULL DEFAULT FALSE,
    roles VARCHAR(10) not null DEFAULT 'user'
);

CREATE TABLE "Jwt" (
    jwtid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    userid bigint not null,
    token TEXT not null,
    token_type VARCHAR(10) NOT NULL,-- Logout, Password Change
    created_at TIMESTAMPTZ DEFAULT NOW(),
    CONSTRAINT fk_user_jwt
        FOREIGN KEY (userid)
        REFERENCES "User"(userid)
        ON DELETE CASCADE
);   