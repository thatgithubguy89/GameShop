import random

users = ["john@gmail.com", "jane@gmail.com", "bill@gmail.com", "bonnie@gmail.com",
         "carl@gmail.com", "cindy@gmail.com", "dan@gmail.com", "diane@gmail.com"]
description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
platforms = ["PC", "PS5", "Wii", "XBox"]
publishers = ["EA", "Sony", "Bethesda", "Valve", "Activision", "Insomniac"]
image_path = "/images/stock.png"
trailer_path = "https://www.youtube.com/embed/7T5l1W6GJHU"
year = 2023


def seed_games(number_of_items: int):
    id = 1

    print(
        f"INSERT INTO [Games] ([Title], [Description], [Platform], [Price], [Publisher], [ReleaseDate], [ImagePath], [TrailerPath], [CreatedBy], [CreateTime], [LastEditTime]) VALUES ")

    for _ in range(number_of_items):
        month = f"{random.randint(1, 9)}"
        day = f"{random.randint(1, 9)}"

        print(f"('Game {id}', " +
              f"'{description}', " +
              f"'{platforms[random.randint(0, 3)]}', " +
              f"59.99, " +
              f"'{publishers[random.randint(0, 5)]}', " +
              f"'{year}/0{month}/0{day}', " +
              f"'{image_path}', " +
              f"'{trailer_path}', " +
              f"'{users[random.randint(0, 7)]}', " +
              f"'{year}/0{month}/0{day}', " +
              f"'{year}/0{month}/0{day}')" +
              f"{';' if id == number_of_items else ','}")

        id += 1


seed_games(50)
