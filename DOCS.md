# 9Recommendations Documentation

Before making any contribution please read [CONTRIBUTING.md](./CONTRIBUTING.md).

To see what is missing please read [ROADMAP.md](./ROADMAP.md).

&nbsp;

---

&nbsp;

## Project structure

- NineRecommendations.Core - Contains all abstraction free of any specific music recommendation provider. This will help with implementation of any kind of new recommendation backend.
- NineRecommendations.Spotify - Contains all implementations of Spotify related music recommendation. (Core recommendation mechanism is handled inside `Recommendation` class).
- NineRecommendations.Front - Contains all application related implementation, handling of requests, sessions, validation, notifications etc.

additionally:

- NineRecommendations.*.UnitTests - Contains all unit and maybe some integration tests.

## Simplified application flow

1. Upon entering home page user is presented with list of all recommendations, handled by `RecommendationController`.
1. When creating new `Recommendation`, `RecommendationController` redirects to `QuestionsController` .
2. Each `Question` is handled in separate action in `QuestionsController` by `QuestionnaireManipulator`.
3. `Questionnaire` is retrieved from `QuestionnaireRepository`, if doesn't exists then it is created.
3. Answer to question is then stored in `Questionnaire`.
4. `Questionnaire` is stored in `QuestionnaireRepository`.
5. If it **is not** the last `Question`, then user gets redirected to next `Question`.
6. If it **is** the last question, then `Recommendation` is created using appropriate `RecommendationBuilder` and user gets redirected to `RecommendationsController`.`
7. Processing of `Recommendation` is in the background, user is free to explore content of the application.
8. Processing finishes and user can now explore proposed recommendation.

> Currently application works in globally open mode, meaning that all recommendations made by any user are visible to anyone using the application.

&nbsp;

---

&nbsp;

## Persistence

At the time of writing this document only in-memory type of repositories are implemented. This means that restarting application discards any recommendation that was present in the system. In the future external persistence mechanism (like SQL, NoSQL or Cache) can be implemented and replace existing ones.

### Replacing repository backend

All repositories are defined trough interface by design. Replacing currently used repository backend requires implementing appropriate interface with new functionality. Then replacing can be done by hand. There is single place where instantiation of repository takes place and it is in service dependencies definition in application `Program.cs` in extension method called `AddPersistence`. There SHOULD bo no more other instantiations taking place (except in tests).

&nbsp;

---

&nbsp;

## Question/Answer relation

### Definition

**Question** - Contains definition of question like phrase that defines it, UUID and list of possible answers.

**Answer** - Defines answer to question and can behave in two ways:

- PassTroughAnswer - specifies that it **is not** the last answer in the chain of questions and points to next one. This allows for using branching questionnaires where based on answers user can be directed in different question-answer path depending on given choices.
- LastQuestion - specifies that this **is** the last answer in the chain of questions and instructs `QuestionnaireManipulator` that processing can now take place.

### Adding new answers

1. Implement `ILastAnswer` or `IPassTroughAnswer`.
2. Make sure that Id is universally unique across the application. Any collision with existing Id can cause unexpected behaviour in application.
3. Add this answer to existing `Question` or implement new one (instructions below).
4. If this is `LastQuestion` make sure to add it's reference to specific `RecommendationBuilder`. This mechanism is required because `RecommendationBuilder` should not process answers that is wasn't supposed to.

### Adding new questions

1. Implement `IQuestion`.
2. Make sure that Id is universally unique across the application. Any collision with existing Id can cause unexpected behaviour in application.
3. Add this question to existing `PassTroughAnswer` or implement new one (instructions above).

### Adding new music recommendation provider

1. Implement `IRecommendationBuilder` and `IRecommendation`.
2. Implement `IAnswer` that will point to this new chain of questions and answers and add it to `EntryQuestion` trough existing `AddEntryQuestion` extension in additional parameter (located in `Program.cs`).
3. There is also possibility to add new questions and answers that will refine your existing recommendations but this functionality is now largely limited and needs more work.