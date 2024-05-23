db.createUser(
        {
            user: "guest",
            pwd: "guest",
            roles: [
                {
                    role: "readWrite",
                    db: "database-example"
                }
            ]
        }
);