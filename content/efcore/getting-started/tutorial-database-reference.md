# Database Reference
This document details the data contained within the example database used throughout this tutorial.
 
## Authors  
 
Id   | FirstName  | LastName
-----|------------|---------
1    | Jack       | London  
2    | Mark       | Twain   
3    | Willa      | Cather   
4    | Frederick  | Douglass
5    | Agatha     | Christie
6    | Virginia   | Woolf   
7    | Frances    | Harper  
8    | Stephen    | Crane   
 
## Books  
 
Id   | AuthorId | Title                           | Genre            | PublicationYear
-----|----------|---------------------------------|------------------|----------------
1    | 6        | Mrs Dalloway                    | Literary         | 1925           
2    | 1        | The Scarlet Plague              | Science Fiction  | 1912           
3    | 5        | The Secret Adversary            | Mystery          | 1922           
4    | 4        | My Bondage and My Freedom       | Narrative        | 1855           
5    | 3        | My Antonia                      | Historical       | 1918           
6    | 3        | O Pioneers!                     | Historical       | 1913           
7    | 2        | Adventures of Huckleberry Finn  | Satire           | 1884           
8    | 2        | The Adventures of Tom Sawyer    | Satire           | 1876           
9    | 7        | Iola Leroy                      | Historical       | 1892           
10   | 5        | Murder on the Orient Express    | Mystery          | 1934           
11   | 1        | The Call of the Wild            | Adventure        | 1903           
12   | 3        | Death Comes for the Archbishop  | Historical       | 1927           
     
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
3     | Holstein   | Nebraska   
4     | Gurley     | Nebraska
5     | Lincoln    | Nebraska
6     | Harrison   | Nebraska
