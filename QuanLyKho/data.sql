CREATE DATABASE Test
GO

USE Test
GO

CREATE TABLE Person (
    id int ,
    name varchar(20),
    CONSTRAINT pessoa_pkey PRIMARY KEY (id)
);

CREATE TABLE Person1 (
    cmnd varchar(11),
    CONSTRAINT pessoaf_pkey PRIMARY KEY (id)
) INHERITS (person);


CREATE TABLE Person2 (
    cccd varchar(14),
    CONSTRAINT pessoaj_pkey PRIMARY KEY (id)
) INHERITS (person);