# Database Reference
This document details the data contained within the example database used throughout this tutorial.
 
## Authors  
 
Id   | FirstName  | LastName
-----|------------|---------
1    | Jack       | London  
2    | Mark       | Twain   
3    | Willa      | Cather   
4    | Frederick  | Douglass
7    | Agatha     | Christie
8    | Virginia   | Woolf   
9    | Frances    | Harper  
10   | Stephen    | Crane   
 
## Books  
 
Id   | AuthorId | Title                           | Genre            | PublicationYear
-----|----------|---------------------------------|------------------|----------------
1    | 9        | Mrs Dalloway                    | Literary         | 1925           
2    | 1        | The Scarlet Plague              | Science Fiction  | 1912           
3    | 8        | The Secret Adversary            | Mystery          | 1922           
4    | 5        | My Bondage and My Freedom       | Narrative        | 1855           
5    | 4        | My Antonia                      | Historical       | 1918           
6    | 4        | O Pioneers!                     | Historical       | 1913           
7    | 2        | Adventures of Huckleberry Finn  | Satire           | 1884           
8    | 2        | The Adventures of Tom Sawyer    | Satire           | 1876           
9    | 10       | Iola Leroy                      | Historical       | 1892           
10   | 8        | Murder on the Orient Express    | Mystery          | 1934           
11   | 1        | The Call of the Wild            | Adventure        | 1903           
12   | 4        | Death Comes for the Archbishop  | Historical       | 1927           
     
## Checkout Records

Id   | BookId | ReaderId | CheckoutDate | ReturnDate | DueDate     
-----|--------|----------|--------------|------------|-----------
1    | 10     | 2        | 12-1-2016    | 12-4-2016  | 12-15-2016
2    | 4      | 6        | 12-5-2016    | 12-31-2016 | 12-19-2016
3    | 1      | 3        | 12-5-2016    | 12-19-2016 | 12-19-2016
4    | 3      | 2        | 12-10-2016   | 12-12-2016 | 12-24-2016
5    | 10     | 4        | 12-16-2016   | 1-1-2017   | 12-30-2016

## Readers

Id   | AddressId | FirstName  | LastName   
-----|-----------|------------|------------
1    | 1         | Andrew     | Salami     
2    | 2         | Rebecca    | Dahlman    
3    | 3         | Ryan       | Helmoski   
4    | 4         | Melia      | Deakin     
5    | 5         | David      | Mikhayelyan
6    | 6         | Aryn       | Huck       

## Addresses
 
Id    | City       | State   
------|------------|---------
1     | Springview | Nebraska
2     | Thurston   | Nebraska
3     | Austin     | Texas   
4     | Gurley     | Nebraska
5     | Lincoln    | Nebraska