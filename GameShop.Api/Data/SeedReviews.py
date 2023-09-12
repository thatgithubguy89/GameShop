import random

users = ["john@gmail.com", "jane@gmail.com", "bill@gmail.com", "bonnie@gmail.com",
         "carl@gmail.com", "cindy@gmail.com", "dan@gmail.com", "diane@gmail.com"]
content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
year = 2023


def seed_reviews(number_of_items: int):
    id = 1

    print(
        f"INSERT INTO [Reviews] ([Title], [Content], [GameId], [CreatedBy], [CreateTime], [LastEditTime]) VALUES ")

    for _ in range(number_of_items):
        month = f"{random.randint(1, 9)}"
        day = f"{random.randint(1, 9)}"

        print(f"('Review {id}', " +
              f"'{content}', " +
              f"'{random.randint(1, 50)}', " +
              f"'{users[random.randint(0, 7)]}', " +
              f"'{year}/0{month}/0{day}', " +
              f"'{year}/0{month}/0{day}')" +
              f"{';' if id == number_of_items else ','}")

        id += 1


seed_reviews(500)
