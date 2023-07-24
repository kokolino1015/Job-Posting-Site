Job Posting Site

1. Introduction:
   We've created a job posting site that benefits both employers and candidates by establishing a quick and simple application process.

2. Overview:
   An employer can publish as many advertisements as they want, but only 10 can be active at the same time. The ad includes a simple text (description), but there are categories for the specific profession: QA, Developer, Manager, DevOps, PM.

3. Installation:
   Step-by-Step guide:
      1. Download the project.
      2. Open the sln file with Visual Studio.
      3. Download postgres and configure the default connection.
      4. Migrite the project by opening the package manager console and execute the following commands:
            Add-Migration (and some name)
            Update-Database
      5. You should be able to start the project

4. Usage:
   Our site makes possible finding a job as well as searching for a person for it. Everyone can make a profile in which they choose a role(employer, candidate). An employer can devise, edit and delete job offers as well as categories for advertisements and candidates can apply(once they apply they cannot apply for the same ad unless they cancel) and cancel their application for a job offer.

5. Features:
   There is a reference (backend) that shows how many active listings we have by category and how many people have applied for each profession (QA, Developer, Manager, DevOps, PM).
   
6. Prerequisites:
- An employer must have a verified account in the system.
- An applicant does not need an account, but if they decide to apply for a particular advertisement, a form must be created and the following information must be provided:
    The candidate's name and on which advertisement they applied.
- A candidate can apply on any job ad, but only once per each.
