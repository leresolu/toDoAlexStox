# toDoAlexStox

## Prerequisites:
- VS Community Edition 2022+
- VS Code (or your favorite front-end editor)
- Firebase account with existing project and Firestore (all free no CC required)
- NodeJS

## API
1. Open VS and open the ToDoAlexStox.sln
2. Get a firebase config file from your firebase project settings (https://support.google.com/firebase/answer/7015592?hl=en#web&zippy=%2Cin-this-article)
3. Modify this in Program.cs to match the file location
   1. `Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"C:\todoalexstox-firebase-adminsdk-uwj1l-728f14f1fc.json");`
4. Build and run the solution

## Front-end
1. Open the ToDoApp folder with VS Code
2. Open a terminal in vs code
   1. `npm install`
   2. `npm run dev`
3. Navigate to localhost:3000
4. Enjoy
