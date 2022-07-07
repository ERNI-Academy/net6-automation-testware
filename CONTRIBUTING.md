[code_of_conduct]: ./CODE_OF_CONDUCT.md
[repo_from_template]: https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-from-a-template
[fork]: https://docs.github.com/en/github/getting-started-with-github/fork-a-repo
[clone]: https://docs.github.com/en/github/creating-cloning-and-archiving-repositories/cloning-a-repository
[push]: https://docs.github.com/en/github/collaborating-with-issues-and-pull-requests/merging-an-upstream-repository-into-your-fork
[pull]: https://docs.github.com/en/github/collaborating-with-issues-and-pull-requests/allowing-changes-to-a-pull-request-branch-created-from-a-fork
[template]: https://github.com/ERNI-Academy/erni-academy-template

# Contributing

This document is a set of guidelines for contributing to this repository.

- [Contributing](#contributing)
  - [Getting started](#getting-started)
  - [1. How to become a contributor](#1-how-to-become-a-contributor)
  - [2. How to create a resource](#2-how-to-create-a-resource)
  - [3. How to contribute](#3-how-to-contribute)
  - [Code of Conduct](#code-of-conduct)

## Getting started

Please note that this project is released with a Contributor [Code of Conduct][code_of_conduct]. By participating in this project you agree to abide by its terms.

All contributions, however small are valued!

If you have an idea to share in the form of Proof of concept, Starterkit or Guideline become a contributor and collaborate with us.

## 1. How to become a contributor

Before starting you must be a ERNI contributor.

Please send us an email to this account so we can add you to our contributors list.

📧 <esp-services@betterask.erni>

## 2. How to create a resource

Create your reposoitory from ERNI Academy Template:

Use this guide [Creating a repository from a template][repo_from_template] to create your repository from ["erni-academy-template"][template].

Please, pay attention to the new repository name and follow this simple schema:

**{type}-{learningPath}-{description}**\.

| Type       | desc             |
|------------|------------------|
| poc        | proof of concept |
| starterkit | Starter Kit      |
| guideline  | Guideline        |
| asset      | Asset            |
| framework  | Framework        |
| training   | Training         |
| action     | Github Action    |

**Examples:**

- poc-frontend-start-with-angular
- starterkit-backend-net-api
- guideline-devops-pipelines

## 3. How to contribute

Whether you want to contribute to your new repository or you want to update ERNI awesome resource list the process is the same:

1. [**Fork the repository**][fork] to your personal Github. Forking is basically an easy way to make a duplicate of the repository to your own account.
2. [**Clone**][clone] the project to your own machine.

    ```sh
    # Clone your fork of the repo into the current directory
    git clone https://github.com/<your-username>/<repo-name>
    # Navigate to the newly cloned directory
    cd <repo-name>
    # Assign the original repo to a remote called "upstream"
    git remote add upstream https://github.com/<upsteam-owner>/<repo-name>
    ```

3. If you cloned a while ago, get the **latest changes** from upstream.

    ```sh
    git checkout <dev-branch>
    git pull upstream <dev-branch>
    ```

4. Create a new **topic branch** (off the main project development branch) to contain your feature, change, or fix.

    ```sh
    git checkout -b <topic-branch-name>
    ```

5. **Commit** changes to your own branch.

    ```sh
    git commit -m 'Add some feature'
    ```

6. **Locally merge** (or rebase) the upstream development branch into your topic branch.

    ```sh
    git pull [--rebase] upstream <dev-branch>
    ```

7. [**Push**][push] your topic branch up to your fork.

    ```sh
    git push origin <topic-branch-name>
    ```

8. Open a [**Pull Request**][pull] with a clear title and description so that we can review your changes.

Thanks for your interest in contributing to this repository.

## Code of Conduct

This project follows a [Code of Conduct][code_of_conduct] in order to ensure an open and welcoming environment.
