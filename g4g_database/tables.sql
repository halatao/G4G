CREATE TABLE Account (
    id_account INTEGER NOT NULL IDENTITY(1,1),
    username   VARCHAR(20) NOT NULL,
    password   VARCHAR(30) NOT NULL
);

ALTER TABLE account ADD CONSTRAINT account_pk PRIMARY KEY ( id_account,
                                                            username );

CREATE TABLE Category (
    name_cat VARCHAR(50) NOT NULL
);

ALTER TABLE category ADD CONSTRAINT category_pk PRIMARY KEY ( name_cat );

CREATE TABLE "Comment" (
    id_comment         INTEGER NOT NULL IDENTITY(1,1),
    text               VARCHAR(250) NOT NULL,
    postedtime         DATE NOT NULL,
    account_id_account INTEGER NOT NULL,
    account_username   VARCHAR(20) NOT NULL,
    content_id_content INTEGER NOT NULL
);

ALTER TABLE "Comment" ADD CONSTRAINT comment_pk PRIMARY KEY ( id_comment );

CREATE TABLE Content (
    id_content              INTEGER NOT NULL IDENTITY(1,1),
    headline                VARCHAR(50) NOT NULL,
    text                    VARCHAR(1000) NOT NULL,
	views INTEGER NULL,
	posted DATETIME NOT NULL,
    account_id_account      INTEGER NOT NULL,
    account_username        VARCHAR(20) NOT NULL,
    subcategory_name_subcat VARCHAR(50) NOT NULL
);

ALTER TABLE content ADD CONSTRAINT content_pk PRIMARY KEY ( id_content );

CREATE TABLE Subcategory (
    name_subcat       VARCHAR(50) NOT NULL,
    category_name_cat VARCHAR(50) NOT NULL,
    icon              VARCHAR(100) NOT NULL
);

ALTER TABLE subcategory ADD CONSTRAINT subcategory_pk PRIMARY KEY ( name_subcat );

ALTER TABLE "Comment"
    ADD CONSTRAINT comment_account_fk FOREIGN KEY ( account_id_account,
                                                    account_username )
        REFERENCES account ( id_account,
                             username );

ALTER TABLE "Comment"
    ADD CONSTRAINT comment_content_fk FOREIGN KEY ( content_id_content )
        REFERENCES content ( id_content );

ALTER TABLE content
    ADD CONSTRAINT content_account_fk FOREIGN KEY ( account_id_account,
                                                    account_username )
        REFERENCES account ( id_account,
                             username );

ALTER TABLE content
    ADD CONSTRAINT content_subcategory_fk FOREIGN KEY ( subcategory_name_subcat )
        REFERENCES subcategory ( name_subcat );

ALTER TABLE subcategory
    ADD CONSTRAINT subcategory_category_fk FOREIGN KEY ( category_name_cat )
        REFERENCES category ( name_cat );