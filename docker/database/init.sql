DROP DATABASE IF EXISTS devworkbench;

DROP TABLE IF EXISTS "Jwt" CASCADE;
DROP TABLE IF EXISTS "User" CASCADE;
DROP TABLE IF EXISTS "Role" CASCADE;

CREATE TABLE "Role" (
    roleid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    rolename VARCHAR(50) UNIQUE NOT NULL
);


CREATE TABLE "User" (
    userid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    userpassword VARCHAR(255) NOT NULL,
    email TEXT UNIQUE NOT NULL,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    isactive BOOLEAN NOT NULL DEFAULT FALSE,
    roleid bigint NOT NULL DEFAULT 1,
    CONSTRAINT fk_user_role
        FOREIGN KEY (roleid) 
        REFERENCES "Role"(roleid)
        ON DELETE RESTRICT -- prevents a role from being deleted
);


CREATE TABLE "Jwt" (
    jwtid bigint GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    userid bigint NOT NULL,
    token TEXT NOT NULL,
    token_type VARCHAR(50) NOT NULL, 
    created_at TIMESTAMPTZ DEFAULT NOW(),
    revoked_at TIMESTAMPTZ, 
    CONSTRAINT fk_user_jwt
        FOREIGN KEY (userid)
        REFERENCES "User"(userid)
        ON DELETE CASCADE
);

-- Currently only using 2 types of roles
INSERT INTO "Role" (rolename) VALUES ('user');
INSERT INTO "Role" (rolename) VALUES ('admin');