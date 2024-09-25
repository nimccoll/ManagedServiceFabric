#===============================================================================
# Microsoft FastTrack for Azure
# Grant Azure Service Fabric Resource Provider Network Contributor rights to a
# Load Balancer
#===============================================================================
# Copyright © Microsoft Corporation.  All rights reserved.
# THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
# OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
# LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
# FITNESS FOR A PARTICULAR PURPOSE.
#===============================================================================
param(
    [Parameter(Mandatory)]$subscriptionId,
    [Parameter(Mandatory)]$resourceGroupName,
    [Parameter(Mandatory)]$loadBalancerName
)
Login-AzAccount
Select-AzSubscription -SubscriptionId $subscriptionId
$sp = Get-AzADServicePrincipal -DisplayName "Azure Service Fabric Resource Provider"
$lb = Get-AzLoadBalancer -Name $loadBalancerName -ResourceGroupName $resourceGroupName
$ra = New-AzRoleAssignment -PrincipalId $sp.Id -RoleDefinitionName "Network Contributor" -Scope $lb.Id